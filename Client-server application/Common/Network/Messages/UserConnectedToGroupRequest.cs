using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network.Messages
{
    public class UserConnectedToGroupRequest
    {
        #region Properties

        public string Username { get; set; }

        public int GroupNumber { get; set; }

        #endregion //Properties

        #region Constructors

        public UserConnectedToGroupRequest(string username, int groupNumber)
        {
            Username = username;
            GroupNumber = groupNumber;
        }

        #endregion //Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(ConnectionRequest),
                Payload = this
            };

            return container;
        }

        #endregion //Methods
    }
}
