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

        public List<string> Users { get; }

        #endregion //Properties

        #region Constructors

        public CreateNewGroupRequest(List<string> users)
        {
            Users = users;
        }

        #endregion //Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(ConnectResponse),
                Payload = this
            };

            return container;
        }

        #endregion //Methods
    }
}
