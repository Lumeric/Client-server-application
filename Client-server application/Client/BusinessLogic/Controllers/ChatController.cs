using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Network;

namespace Client.BusinessLogic
{
    public class ChatController : IChatController
    {
        #region Constants



        #endregion //Constants

        #region Events

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<LoginReceivedEventArgs> LoginReceived;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<MessageHistoryReceivedEventArgs> MessageHistoryReceived;
        public event EventHandler<FilteredLogsReceivedEventArgs> FilteredLogsReceived;
        public event EventHandler<UsersReceivedEventArgs> UsersReceived;
        public event EventHandler<GroupsReceivedEventArgs> GroupsReceived;

        #endregion //Events

        #region Fields

        private readonly ITransport _transport;

        #endregion //Fields

        #region Properties

        public string Username { get; set; }

        #endregion //Properties

        #region Constructors

        public ChatController(ITransport transport)
        {
            _transport = transport;
            _transport.ConnectionStateChanged += OnConnectionStateChanged;
            _transport.LoginReceived += OnLoginReceived;
            _transport.MessageReceived += OnMessageReceived;
            _transport.UsersReceived += OnUsersReceived;
            _transport.MessageHistoryReceived += OnMessageHistoryReceived;
            _transport.FilteredLogsReceived += OnFilteredLogsReceived;
            _transport.GroupsReceived += OnGroupsReceived;
        }

        #endregion //Constructors

        #region Methods

        public void Send(string username, string message, string groupname)
        {

        }

        public void LeaveGroup(string groupName)
        {
            //_transport.Send();
        }

        public void Disconnect()
        {
            Username = string.Empty;
            _transport.Disconnect();
        }

        private void OnConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Username))
            {
                ConnectionStateChanged?.Invoke(this, e);
            }
        }

        private void OnLoginReceived(object sender, LoginReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Login))
            {
                LoginReceived?.Invoke(this, e);
            }
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnUsersReceived(object sender, UsersReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnMessageHistoryReceived(object sender, MessageHistoryReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnFilteredLogsReceived(object sender, FilteredLogsReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnGroupsReceived(object sender, GroupsReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Connect(string address, string port)
        {
            throw new NotImplementedException();
        }

        public void Login(string username)
        {
            throw new NotImplementedException();
        }

        #endregion //Methods
    }
}
