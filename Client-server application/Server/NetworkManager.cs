using System;
using System.Net;

using Common.Network;


namespace Server
{
    public class NetworkManager
    {
        #region Constants
        private const int WS_PORT = 65000;
        private const int TCP_PORT = 65001;

        #endregion //Constants

        #region Fields

        private readonly WsServer _wsServer;
        private IPAddress _ip;
        private int _port;
        private string _protocol;

        #endregion //Fields

        #region Constructors

        public NetworkManager()
        {
            _wsServer = new WsServer(new IPEndPoint(IPAddress.Any, WS_PORT));

            //_tcpServer = new TcpServer(new IPEndPoint(IPAddress.Any, TCP_PORT));
        }

        #endregion //Constuctors

        #region Methods

        public void Start()
        {
            Console.WriteLine("Start");
        }

        public void Stop()
        {
            Console.WriteLine("Stop");
        }

        #endregion //Methods
    }
}
