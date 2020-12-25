namespace Common.Network
{
    using Messages;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using WebSocketSharp.Server;
    using Newtonsoft.Json.Linq;

    public class WsServer
    {
        #region Fields

        private readonly IPEndPoint _listenAddress;
        private readonly ConcurrentDictionary<Guid, WsConnection> _connections;

        private WebSocketServer _server;

        #endregion //Fields

        #region Events

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<GroupCreatedEventArgs> GroupCreated;
        public event EventHandler<GroupRemovedEventArgs> GroupRemoved;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<UserConnectedEventArgs> UserConnected;
        public event EventHandler<UserConnectedToGroupEventArgs> UserConnectedToGroup;
        public event EventHandler<UserDisconnectedEventArgs> UserDisconnected;
        public event EventHandler<ErrorReceivedEventArgs> ErrorReceived;

        #endregion //Events

        #region Constructors

        public WsServer(IPEndPoint listenAddress)
        {
            _listenAddress = listenAddress;
            _connections = new ConcurrentDictionary<Guid, WsConnection>();
        }

        #endregion //Constructors

        #region Methods

        public void Start()
        {
            _server = new WebSocketServer(_listenAddress.Address, _listenAddress.Port, false);
            _server.AddWebSocketService<WsConnection>("/",
                client =>
                {
                    client.AddServer(this);
                });
                _server.Start();
        }
            
        public void Stop()
        {
            _server?.Stop();
            _server = null;

            var connections = _connections.Select(item => item.Value).ToArray();
            foreach (var connection in connections)
            {
                connection.Close();
            }

            _connections.Clear();
        }

        public void Send(List<Guid> listClientId, MessageContainer message)
        {
            foreach (var id in listClientId)
            {
                if (!_connections.TryGetValue(id, out WsConnection connection))
                    continue;

                connection.Send(message);
            }
        }

        internal void HandleMessage(Guid clientId, MessageContainer container)
        {
            if (!_connections.TryGetValue(clientId, out WsConnection connection))
                return;

            switch (container.Identifier)
            {
                case nameof(ConnectRequest):
                    {
                        var connectRequest = ((JObject)container.Payload).ToObject(typeof(ConnectRequest)) as ConnectRequest;
                        var connectResponse = new ConnectResponse { Result = ResultCode.Ok, IsSuccess = true };

                        if (_connections.Values.Any(item => item.Username == connectRequest.Username))
                        {
                            string reason = $"{connectRequest.Username} is already logged";
                            connectResponse.IsSuccess = false;
                            connectResponse.Reason = reason;
                            connectResponse.Result = ResultCode.Failure;
                            connection.Send(connectResponse.GetContainer());
                            ErrorReceived?.Invoke(this, new ErrorReceivedEventArgs(ErrorType.UsernameError, reason, DateTime.Now));
                        }
                        else
                        {
                            connection.Username = connectRequest.Username;
                            connectResponse.ActiveUsers = _connections.Select(item => item.Value.Username).ToList();
                            connection.Send(connectResponse.GetContainer());
                            ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(connection.Username, true));
                            UserConnected?.Invoke(this, new UserConnectedEventArgs(connection.Username, clientId));
                        }
                        break;
                    }
                case nameof(DisconnectRequest):
                    {
                        var disconnectRequest = ((JObject)container.Payload).ToObject(typeof(ConnectRequest)) as ConnectRequest;
                        UserDisconnected?.Invoke(this, new UserDisconnectedEventArgs(connection.Username));
                        break;
                    }
                case nameof(MessageRequest):
                    {
                        var messageRequest = ((JObject)container.Payload).ToObject(typeof(MessageRequest)) as MessageRequest;
                        MessageReceived?.Invoke(this, new MessageReceivedEventArgs(connection.Username, messageRequest.Message, messageRequest.Group));
                        break;
                    }
                case nameof(CreateNewGroupRequest):
                    {
                        var createNewGroupRequest = ((JObject)container.Payload).ToObject(typeof(CreateNewGroupRequest)) as CreateNewGroupRequest;
                        GroupCreated?.Invoke(this, new GroupCreatedEventArgs(connection.Username, createNewGroupRequest.Users));
                        break;
                    }
                case nameof(ConnectUserToGroupRequest):
                    {
                        var connectUserToGroupRequest = ((JObject)container.Payload).ToObject(typeof(ConnectUserToGroupRequest)) as ConnectUserToGroupRequest;
                        UserConnectedToGroup?.Invoke(this, new UserConnectedToGroupEventArgs(connection.Username, connectUserToGroupRequest.GroupNumber));
                        break;
                    }
                case nameof(RemoveGroupRequest):
                    {
                        var removeGroupRequest = ((JObject)container.Payload).ToObject(typeof(RemoveGroupRequest)) as RemoveGroupRequest;
                        GroupRemoved?.Invoke(this, new GroupRemovedEventArgs(connection.Username, removeGroupRequest.GroupNumber));
                        break;
                    }               
            }
        }

        internal void AddConnection(WsConnection connection)
        {
            _connections.TryAdd(connection.Id, connection);
        }

        internal void RemoveConnection(Guid connectionId)
        {
            _connections.TryRemove(connectionId, out WsConnection connection);
        }

        #endregion Methods


    }
}
