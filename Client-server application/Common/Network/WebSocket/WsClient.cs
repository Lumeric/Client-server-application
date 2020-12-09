using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using WebSocketSharp;
using WebSocketSharp.Server;



namespace Common.Network
{
    public class WsClient : ITransport
    {
        #region Fields
        #endregion Fields

        #region Properties
        #endregion Properties

        #region Events

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        #endregion Events

        #region Constructors
        #endregion Constructors

        #region Methods

        public void Connect(string address, string port)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Login(string login)
        {
            throw new NotImplementedException();
        }

        public void Send(string message)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
