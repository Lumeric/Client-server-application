using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network.Messages
{
    public class LeaveGroupRequest
    {
        #region Properties

        public string Groupname { get; set; }

        #endregion // Properties

        #region Constructors

        public LeaveGroupRequest(string groupname)
        {
            Groupname = groupname;
        }

        #endregion // Constructors

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

        #endregion // Methods
    }
}
