using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network.Messages
{
    public class CreateNewGroupRequest
    {
        #region Properties

        public string Groupname { get; set; }
        public List<string> Users { get; }

        #endregion //Properties

        #region Constructors

        public CreateNewGroupRequest(string groupname, List<string> users)
        {
            Groupname = groupname;
            Users = users;
        }

        #endregion //Constructors

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

        #endregion //Methods
    }
}
