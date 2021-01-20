using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network.Messages
{
    public class GroupBroadcast
    {
        #region Properties

        public Dictionary<string, List<string>> Groupname { get; set; }

        #endregion // Properties

        #region Constructors

        public GroupBroadcast(Dictionary<string, List<string>> groupname)
        {
            Groupname = groupname;
        }

        #endregion // Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(GroupBroadcast),
                Payload = this
            };

            return container;
        }

        #endregion // Methods
    }
}
