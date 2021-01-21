using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ViewModels;
using Common.Network;
using Common.Network.Messages;

namespace Client.BusinessLogic
{
    public class LoginHandler : ILoginHandler
    {
        #region Events

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<ErrorReceivedEventArgs> ErrorReceived;

        #endregion // Events

        #region Fields

        private ITransport _transport;

        #endregion // Fields

        #region Properties



        #endregion // Properties

        #region Constructors

        public LoginHandler(ITransport transport)
        {
            _transport = transport;

            _transport.ConnectionStateChanged += OnConnectionStateChanged;
            _transport.ErrorReceived += OnErrorReceived;
        }

        #endregion // Constructors

        #region Methods

        public void ConnectUser(string address, string port)
        {
            _transport.Connect(address, port);
        }

        public void LoginUser(string username)
        {
            _transport.Username = username;
            _transport.Send(new ConnectionRequest(username).GetContainer());
        }

        public void DisconnectUser()
        {
            _transport.Disconnect();
        }

        private void OnConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            ConnectionStateChanged?.Invoke(this, e);
        }

        private void OnErrorReceived(object sender, ErrorReceivedEventArgs e)
        {
            ErrorReceived?.Invoke(this, e);
        }

        #endregion // Methods
    }
}
