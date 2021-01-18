using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ViewModels;
using Common.Network;

namespace Client.BusinessLogic
{
    public class LoginController : ILoginController
    {
        #region Constants



        #endregion //Constants

        #region Events

        public event EventHandler<UserConnectedEventArgs> UserConnected;
        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<ErrorReceivedEventArgs> ErrorReceived;

        #endregion //Events

        #region Fields

        private ITransport _transport;
        private List<TransportType> _sockets;
        private TransportType _selectedSocket;
        private ObservableCollection<string> _eventLog;
        private string _username;

        #endregion //Fields

        #region Properties



        #endregion //Properties

        #region Constructors

        public LoginController()
        {
            _sockets = new List<TransportType>();
            _selectedSocket = new TransportType();
            _eventLog = new ObservableCollection<string>();
            _sockets.Add(TransportType.WebSocket);
            _sockets.Add(TransportType.TcpSocket);
            _selectedSocket = TransportType.WebSocket;
        }

        #endregion //Constructors

        #region Methods

        public void ConnectUser(string address, string port)
        {
            try
            {
                _transport = TransportFactory.Create((TransportType)_selectedSocket);
                //_transport.ConnectionStateChanged += OnConnectionStateChanged;
                //_transport.MessageReceived += OnMessageReceived;
                _transport.Connect(address, port);
                LoginUser(_username); //viewModel or controller
            }
            catch (Exception ex)
            {
                _eventLog.Add(ex.Message);
            }
        }

        public void LoginUser(string username) //event
        {
            //_transport?Login();
        }

        private void OnConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            ConnectionStateChanged?.Invoke(this, e);
        }

        public void DisconnectUser()
        {
            throw new NotImplementedException();
        }


        #endregion //Methods
    }
}
