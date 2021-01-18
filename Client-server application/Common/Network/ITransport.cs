using Common.Network.Messages;
using System;
using System.Collections.Generic;

namespace Common.Network
{
    public interface ITransport
    {
        #region Events

       event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
       event EventHandler<LoginReceivedEventArgs> LoginReceived;
       event EventHandler<MessageReceivedEventArgs> MessageReceived;
       event EventHandler<MessageHistoryReceivedEventArgs> MessageHistoryReceived;
       event EventHandler<FilteredLogsReceivedEventArgs> FilteredLogsReceived;
       event EventHandler<UsersReceivedEventArgs> UsersReceived;
       event EventHandler<GroupsReceivedEventArgs> GroupsReceived;

        #endregion //Events

        #region Methods

        void Connect(string address, string port);

        void Disconnect();

        void Login(string login);

        void Send(List<Guid> listClientId, MessageContainer message);

        #endregion //Methods
    }
}
