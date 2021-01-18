using Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public interface IChatController
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

        void Send(string username, string message, string groupname);

        void LeaveGroup(string groupname);

        void Disconnect();

        #endregion //Methods
    }
}
