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
    using System.Timers;

    public class WsServer
    {
        #region Fields

        private readonly IPEndPoint _listenAddress;
        private readonly ConcurrentDictionary<Guid, WsConnection> _connections;
        private WebSocketServer _server;

        #endregion // Fields

        #region Events

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionReceived;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<GroupCreatedEventArgs> GroupCreated;
        public event EventHandler<GroupLeavedEventArgs> GroupLeaved;      
        public event EventHandler<ErrorReceivedEventArgs> ErrorReceived;
        public event EventHandler<FiltrationReceivedEventArgs> FiltrationReceived;

        #endregion // vents

        #region Constructors

        public WsServer(IPEndPoint listenAddress)
        {
            _listenAddress = listenAddress;
            _connections = new ConcurrentDictionary<Guid, WsConnection>();
        }

        #endregion // Constructors

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

        public void Send(string source, string target, MessageContainer message)
        {
            if (String.IsNullOrEmpty(target))
            {
                foreach (var connection in _connections)
                {
                    connection.Value?.Send(message);
                }
            }
            else
            {
                foreach (var connection in _connections)
                {
                    if (connection.Value.Username == target || connection.Value.Username == source)
                    {
                        connection.Value?.Send(message);
                    }
                }
            }
        }

        internal void HandleMessage(Guid clientId, MessageContainer container)
        {
            if (!_connections.TryGetValue(clientId, out WsConnection connection))
                return;

            switch (container.Identifier)
            {
                case nameof(ConnectionRequest):
                    var connectionRequest = ((JObject)container.Payload).ToObject(typeof(ConnectionRequest)) as ConnectionRequest;
                    var connectionResponse = new ConnectionResponse
                    {
                        Result = ResultCode.Ok,
                        IsSuccess = true,
                    };
                    if (_connections.Values.Any(c => c.Username == connectionRequest.Username))
                    {
                        string reason = $"User '{connectionRequest.Username}' is already logged in.";
                        connectionResponse.Result = ResultCode.Failure;
                        connectionResponse.IsSuccess = false;
                        connectionResponse.Reason = reason;
                        connection.Send(connectionResponse.GetContainer());
                        ErrorReceived?.Invoke(this, new ErrorReceivedEventArgs(reason, DateTime.Now));
                    }
                    else
                    {
                        connection.Username = connectionRequest.Username;
                        connectionResponse.ActiveUsers = _connections.Where(c => c.Value.Username != null).Select(u => u.Value.Username).ToList();
                        connection.Send(connectionResponse.GetContainer());
                        ConnectionReceived?.Invoke(this, new ConnectionStateChangedEventArgs(connection.Username, true, DateTime.Now));
                    }
                    break;
                case nameof(MessageRequest):
                    var messageRequest = ((JObject)container.Payload).ToObject(typeof(MessageRequest)) as MessageRequest;
                    MessageReceived?.Invoke(this, new MessageReceivedEventArgs(connection.Username, messageRequest.Target, messageRequest.Message, messageRequest.Groupname, DateTime.Now));
                    break;
                case nameof(FiltrationRequest):
                    var filtrationRequest = ((JObject)container.Payload).ToObject(typeof(FiltrationRequest)) as FiltrationRequest;
                    FiltrationReceived?.Invoke(this, new FiltrationReceivedEventArgs(connection.Username, filtrationRequest.FirstDate, filtrationRequest.SecondDate, filtrationRequest.EventType));
                    break;
                case nameof(CreateGroupRequest):
                    var createGroupRequest = ((JObject)container.Payload).ToObject(typeof(CreateGroupRequest)) as CreateGroupRequest;
                    createGroupRequest.UserList.Add(connection.Username);
                    GroupCreated?.Invoke(this, new GroupCreatedEventArgs(createGroupRequest.Groupname, createGroupRequest.UserList));
                    break;
                case nameof(LeaveGroupRequest):
                    var leaveGroupRequest = ((JObject)container.Payload).ToObject(typeof(LeaveGroupRequest)) as LeaveGroupRequest;
                    GroupLeaved?.Invoke(this, new GroupLeavedEventArgs(connection.Username, leaveGroupRequest.Groupname));
                    break;
            }
        }

        internal void AddConnection(WsConnection connection)
        {
            _connections.TryAdd(connection.Id, connection);
        }

        internal void RemoveConnection(Guid connectionId)
        {
            if (_connections.TryRemove(connectionId, out WsConnection connection) && !string.IsNullOrEmpty(connection.Username))
            {
                ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(connection.Username, false, DateTime.Now));
                ConnectionReceived?.Invoke(this, new ConnectionStateChangedEventArgs(connection.Username, false, DateTime.Now));
            }

        }

        #endregion // Methods
    }
}
