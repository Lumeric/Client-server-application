using Common.Network;
using Common.Network.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public class ConnectionController : IConnectionController
    {
        #region Constants



        #endregion //Constants

        #region Events

        public event EventHandler<UserConnectedEventArgs> UserConnected;

        #endregion //Events

        #region Fields

        private readonly ConcurrentQueue<MessageContainer> _sendQueue;

        private WsClient _socket;

        private int _sending;

        #endregion //Fields

        #region Properties



        #endregion //Properties

        #region Constructors

        public ConnectionController()
        {
        }

        #endregion //Constructors

        #region Methods

        public void Connect(string address, string port)
        {

        }

        #endregion //Methods
    }
}
