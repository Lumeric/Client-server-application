using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class TcpServer
    {
        #region Fields

        private IPEndPoint iPEndPoint;

        #endregion Fields

        #region Events
        #endregion Events

        #region Constructors

        public TcpServer(IPEndPoint iPEndPoint)
        {
            this.iPEndPoint = iPEndPoint;
        }

        #endregion Constructors

        #region Methods

        public void Start()
        {

        }

        public void Stop()
        {

        }

        public void Send(string message)
        {

        }

        internal void HandleMessage(IPEndPoint remoteEndpoint, byte[] packet)
        {

        }

        private void ConnectionAccepting()
        {

        }

        private void ConnectionAccepted(object sender, SocketAsyncEventArgs e)
        {
            //
            ConnectionAccepting();
        }

        internal void RemoveConnection(IPEndPoint remoteEndpoint)
        {

        }

        #endregion Methods

    }
}
