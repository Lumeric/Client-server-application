using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Network;
using Common.Network.Messages;

namespace Client.BusinessLogic
{
    public class ChatHandler : IChatHandler
    {
        #region Events

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        //public event EventHandler<ConnectionStateChangedEventArgs> ConnectionReceived; // rebuild with this  name the second ones
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<MessageHistoryReceivedEventArgs> MessageHistoryReceived;
        public event EventHandler<FilteredLogsReceivedEventArgs> FilteredLogsReceived;
        public event EventHandler<UsersReceivedEventArgs> UsersReceived;
        public event EventHandler<GroupsReceivedEventArgs> GroupsReceived;

        #endregion // Events

        #region Fields

        private readonly ITransport _transport;

        #endregion //Fields

        #region Properties

        public string Username { get; set; }

        #endregion //Properties

        #region Constructors

        public ChatHandler(ITransport transport)
        {
            _transport = transport;

            _transport.ConnectionStateChanged += OnConnectionStateChanged;
            _transport.ConnectionStateChanged += OnConnectionReceived;
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
            if (username == "General")
            {
                username = String.Empty;
            }

            _transport.Send(new MessageRequest(username, message, groupname).GetContainer());
        }

        public void LeaveGroup(string groupname)
        {
            _transport.Send(new LeaveGroupRequest(groupname).GetContainer());
        }

        public void Disconnect()
        {
            Username = String.Empty;
            _transport.Disconnect();
        }

        //little bugs
        private void OnConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Username))
            {
                ConnectionStateChanged?.Invoke(this, e);
            }
        }

        private void OnConnectionReceived(object sender, ConnectionStateChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Username))
            {
                ConnectionStateChanged?.Invoke(this, e);
            }
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        private void OnUsersReceived(object sender, UsersReceivedEventArgs e)
        {
            UsersReceived?.Invoke(this, e);
        }

        private void OnMessageHistoryReceived(object sender, MessageHistoryReceivedEventArgs e)
        {
            MessageHistoryReceived?.Invoke(this, e);
        }

        private void OnFilteredLogsReceived(object sender, FilteredLogsReceivedEventArgs e)
        {
            FilteredLogsReceived?.Invoke(this, e);
        }

        private void OnGroupsReceived(object sender, GroupsReceivedEventArgs e)
        {
            GroupsReceived?.Invoke(this, e);
        }

        #endregion //Methods
    }
}
