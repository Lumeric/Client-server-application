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

        private IPEndPoint _listenAddress;
        private readonly ConcurrentDictionary<Guid, WsConnection> _connections;

        private WebSocketServer _server;

        #endregion //Fields

        #region Events

        public event EventHandler<GroupCreatedEventArgs> GroupCreated;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<UserConnectedEventArgs> UserConnected;
        public event EventHandler<UserDisconnectedEventArgs> UserDisconnected;
        public event EventHandler<UserConnectedToGroupEventArgs> UserConnectedToGroup;

        #endregion //Events

        #region Constructors

        public WsServer(IPEndPoint listenAddress)
        {

            _server = new WebSocketServer(listenAddress.Address, listenAddress.Port, false);
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

        internal void HandleMessage(Guid userId, MessageContainer container)
        {

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
