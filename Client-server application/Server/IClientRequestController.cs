using Common.Network;
using Common.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public interface IClientRequestController
    {
        #region Events
        
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event EventHandler<UserConnectedEventArgs> UserConnected;
        event EventHandler<UserConnectedToGroupEventArgs> UserConnectedToGroup;
        event EventHandler<GroupCreatedEventArgs> GroupCreated;

        #endregion //Events
        #region Methods

        void GetPacket(Guid iserID, MessageContainer container);

        #endregion //Methods
    }
}
