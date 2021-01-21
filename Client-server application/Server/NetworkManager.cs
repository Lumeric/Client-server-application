using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

using Common.Network;
using Common.Network.Messages;
using Server.Handlers;

namespace Server
{
    public class NetworkManager
    {
        #region Fields

        private readonly WsServer _wsServer;
        private readonly TransportTypes _transport;
        private readonly IPAddress _ip;
        private readonly int _port;
        private readonly ConnectionStringSettings _connectionString;

        private readonly RequestHandler _requestHander;

        #endregion //Fields

        #region Constructors

        public NetworkManager()
        {
            ServerConfiguration configuration = new ServerConfiguration("configuration.xml");

            _transport = configuration.Transport;
            _ip = configuration.Ip;
            _port = configuration.Port;
            _connectionString = configuration.ConnectionSettings;

            DatabaseHandler dbHandler = new DatabaseHandler(_connectionString);

            switch(_transport)
            {
                case 0:
                    {
                        _wsServer = new WsServer(new IPEndPoint(_ip, _port));
                        _wsServer.ConnectionStateChanged += OnConnectionStateChanged;
                        _wsServer.ConnectionReceived += OnConnectionReceived;
                        _wsServer.MessageReceived += OnMessageReceived;
                        _wsServer.ErrorReceived += OnErrorReceived;
                        _wsServer.FiltrationReceived += OnFiltrationReceived;
                        _wsServer.GroupCreated += OnGroupCreated;
                        _wsServer.GroupLeaved += OnGroupLeaved;

                        break;
                    }
                default:
                    {
                        Console.WriteLine("Unexpected error");
                        break;
                    }
            }

            _requestHander = new RequestHandler(dbHandler);
        }

        #endregion //Constuctors

        #region Methods

        public void Start()
        {
            _wsServer.Start();
            Console.WriteLine($"{_transport}Server: {_ip}:{_port} started");
        }

        public void Stop()
        {          
            _wsServer.Stop();
            Console.WriteLine("Server stopped");
        }

        private void OnConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            string userState = e.IsConnected ? "connected." : "disconnected.";
            string message = $"{e.Username} {userState}.";

            if (e.IsConnected)
            {
                var messageHistory = _requestHander.GetGroupMessages(e.Username);
                var users = _requestHander.GetUsers();
                var groups = _requestHander.GetGroups(e.Username);

                _wsServer.Send(String.Empty, e.Username, new MessageHistoryResponse(messageHistory).GetContainer());
                _wsServer.Send(String.Empty, e.Username, new UserListResponse(users).GetContainer());
                _wsServer.Send(String.Empty, e.Username, new GroupListResponse(groups).GetContainer());
            }

            _requestHander.AddEvent(EventType.Event, message, e.Date);

            Console.WriteLine($"{DateTime.Now} - {message}");

            _wsServer.Send(e.Username, String.Empty, new MessageBroadcast(e.Username, String.Empty, userState, String.Empty, DateTime.Now).GetContainer());
        }

        private void OnConnectionReceived(object sender, ConnectionReceivedEventArgs e)
        {
            _requestHander.AddUser(e.Username);

            _wsServer.Send(e.Username, String.Empty, new ConnectionBroadcast(e.Username, e.IsConnected, DateTime.Now).GetContainer());
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            string target = e.Target;

            if (!String.IsNullOrEmpty(e.Groupname))
            {
                target = String.Empty;
            }

            if (e.Target == null)
            {
                Console.WriteLine($"{e.Username}: {e.Message}");
            }
            else
            {
                Console.WriteLine($"{e.Username} -> {e.Target}: {e.Message}");
            }

            _requestHander.AddMessage(e.Username, e.Target, e.Message, e.Date);

            _wsServer.Send(e.Username, target, new MessageBroadcast(e.Username, target, e.Message, e.Groupname, DateTime.Now).GetContainer());
        }


        private void OnErrorReceived(object sender, ErrorReceivedEventArgs e)
        {
            _requestHander.AddEvent(EventType.Error, e.Message, e.Date);
        }

        private void OnFiltrationReceived(object sender, FiltrationReceivedEventArgs e)
        {
            var filteredLogs = _requestHander.GetEventLog(e.FirstDate, e.SecondDate, e.EventType);
            _wsServer.Send(String.Empty, e.Username, new FiltrationResponse(filteredLogs).GetContainer());
        }

        private void OnGroupCreated(object sender, GroupCreatedEventArgs e)
        {
            _requestHander.AddGroup(e.Groupname, e.UserList);
            _wsServer.Send(String.Empty, String.Empty, new GroupBroadcast(new Dictionary<string, List<string>>() { { e.Groupname, e.UserList } }).GetContainer());
        }

        private void OnGroupLeaved(object sender, GroupLeavedEventArgs e)
        {
            _requestHander.LeaveGroup(e.Username, e.Groupname);
        }

        #endregion //Methods
    }
}
