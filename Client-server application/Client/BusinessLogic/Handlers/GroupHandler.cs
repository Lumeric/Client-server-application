using Common.Network;
using Common.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public class GroupHandler : IGroupHandler
    {
        #region Fields

        private readonly ITransport _transport;

        #endregion // Fields

        #region Constructors

        public GroupHandler(ITransport transport)
        {
            _transport = transport;
        }

        #endregion // Constructors

        #region Methods

        public void CreateGroupRequest(string groupname, List<string> users)
        {
            _transport.Send(new CreateGroupRequest(groupname, users).GetContainer());
        }

        #endregion // Methods
    }
}
