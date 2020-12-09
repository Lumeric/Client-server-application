using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network.Messages
{
    public class DisconnectRequest
    {
        #region Properties

        public string Username { get; }

        #endregion //Properties


        #region Constructors

        public DisconnectRequest(string username)
        {
            Username = username;
        }

        #endregion //Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(DisconnectRequest),
                Payload = this
            };

            return container;
        }

        #endregion //Methods
    }
}
