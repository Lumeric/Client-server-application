using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network.Messages
{
    public class CreateGroupRequest
    {
        #region Properties

        public string Groupname { get; set; }

        public List<string> UserList { get; set; }

        #endregion // Properties

        #region Constructors

        public CreateGroupRequest(string groupname, List<string> userList)
        {
            Groupname = groupname;
            UserList = userList;
        }

        #endregion // Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(ConnectionResponse),
                Payload = this
            };

            return container;
        }

        #endregion // Methods
    }
}
