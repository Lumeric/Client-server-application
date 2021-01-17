using Common.Network;
using Common.Network.Messages;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ClientRequestController : IClientRequestController
    {
        #region Events

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<UserConnectedEventArgs> UserConnected;
        public event EventHandler<UserConnectedToGroupEventArgs> UserConnectedToGroup;
        public event EventHandler<GroupCreatedEventArgs> GroupCreated;

        #endregion //Events

        #region Methods

        public void GetPacket(Guid userID, MessageContainer container)
        {
            switch (container.Identifier)
            {
                case nameof(ConnectionRequest):
                    {
                        var connectRequest = ((JObject)container.Payload).ToObject(typeof(ConnectionRequest)) as ConnectionRequest;
                        UserConnected?.Invoke(this, new UserConnectedEventArgs(connectRequest.Username, userID));
                        break;
                    }
                case nameof(MessageRequest):
                    {
                        var messageRequest = ((JObject)container.Payload).ToObject(typeof(MessageRequest)) as MessageRequest;
                        MessageReceived?.Invoke(this, new MessageReceivedEventArgs(messageRequest.Username, messageRequest.Message, messageRequest.Group, messageRequest.Date));
                        break;
                    }
                case nameof(UserConnectedToGroupRequest):
                    {
                        var connectionToGroupRequest = ((JObject)container.Payload).ToObject(typeof(UserConnectedToGroupRequest)) as UserConnectedToGroupRequest;
                        UserConnectedToGroup?.Invoke(this, new UserConnectedToGroupEventArgs(connectionToGroupRequest.Username, connectionToGroupRequest.GroupNumber));
                        break;
                    }
                case nameof(CreateNewGroupRequest):
                    {
                        var createNewGroupRequest = ((JObject)container.Payload).ToObject(typeof(CreateNewGroupRequest)) as CreateNewGroupRequest;
                        GroupCreated?.Invoke(this, new GroupCreatedEventArgs(createNewGroupRequest.Groupname, createNewGroupRequest.Users));
                        break;
                    }
            }
        }

        #endregion //Methods
    }
}
