﻿using System;
using System.Configuration;
using System.Net;

using Common.Network;
using Server.Handlers;

namespace Server
{
    public class NetworkManager
    {
        #region Fields

        private readonly WsServer _wsServer;
        private readonly IPAddress _ip;
        private readonly int _port;
        private readonly TransportTypes _transport;
        private readonly ConnectionStringSettings _connectionString;

        private RequestHandler _messageHandler;

        #endregion //Fields

        #region Constructors

        public NetworkManager()
        {
            ServerConfiguration configuration = new ServerConfiguration("configuration.xml");

            _transport = configuration.Transport;
            _ip = configuration.Ip;
            _port = configuration.Port;
            _connectionString = configuration.ConnectionSettings;

            DatabaseHandler dbController = new DatabaseHandler(_connectionString);

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

            _messageHandler = new RequestHandler(dbController);
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
            string userState = e.IsConnected ? "подлючился." : "отключился.";
            string message = $"{e.Username} {userState}.";

            if (e.IsConnected)
            {
                var messageHistory = _messageHandler.GetGroupMessages(e.Username);
                var users = 
            }
        }

        private void HandleConnectionReceived(object sender, ConnectionStateChangedEventArgs e)
        {
            
        }

        private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion //Methods
    }
}
