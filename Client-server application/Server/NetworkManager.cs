using System;
using System.Configuration;
using System.Net;

using Common.Network;
using Server.Controllers;

namespace Server
{
    public class NetworkManager
    {
        #region Fields

        private readonly WsServer _wsServer;
<<<<<<< Updated upstream
        private readonly TcpServer _tcpServer;
        private IPAddress _ip;
        private int _port;
        private string _protocol;
=======
        private readonly IPAddress _ip;
        private readonly int _port;
        private readonly TransportTypes _transport;
        private readonly ConnectionStringSettings _connectionString;

        private MessageHandler _messageHandler;
>>>>>>> Stashed changes

        #endregion //Fields

        #region Constructors

        public NetworkManager()
        {
            ServerConfiguration configuration = new ServerConfiguration("configuration.xml");

            _transport = configuration.Transport;
            _ip = configuration.Ip;
            _port = configuration.Port;
            _connectionString = configuration.ConnectionSettings;

            DatabaseController dbController = new DatabaseController(_connectionString);

            switch(_transport)
            {
                case 0:
                    {
                        _wsServer = new WsServer(new IPEndPoint(_ip, _port));
                        _wsServer.ConnectionStateChanged += HandleConnectionStateChanged;
                        _wsServer.ConnectionStateChanged += HandleConnectionReceived;
                        _wsServer.MessageReceived += HandleMessageReceived;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Unexpected error");
                        break;
                    }
            }

            _messageHandler = new MessageHandler(dbController);
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

        private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HandleConnectionReceived(object sender, ConnectionStateChangedEventArgs e)
        {
            string userState = e.IsConnected ? "подлючился." : "отключился.";
            string message = $"{e.Username} {userState}.";

            if (e.IsConnected)
            {
                var messageHistory = _messageHandler.GetGroupMessages(e.Username);

            }
        }

        private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion //Methods
    }
}
