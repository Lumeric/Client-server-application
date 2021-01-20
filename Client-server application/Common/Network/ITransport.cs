using Common.Network.Messages;
using System;

namespace Common.Network
{
    public interface ITransport
    {
        #region Properties

        string Username { get; set; }

        #endregion // Properties

        #region Events

        event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
       event EventHandler<MessageReceivedEventArgs> MessageReceived;
       event EventHandler<MessageHistoryReceivedEventArgs> MessageHistoryReceived;
       event EventHandler<ErrorReceivedEventArgs> ErrorReceived;
       event EventHandler<UsersReceivedEventArgs> UsersReceived;
       event EventHandler<GroupsReceivedEventArgs> GroupsReceived;
       event EventHandler<FilteredLogsReceivedEventArgs> FilteredLogsReceived;

        #endregion //Events

        #region Methods

        void Connect(string address, string port);

        void Send(MessageContainer messageContainer);

        void Disconnect();

        #endregion //Methods
    }
}
