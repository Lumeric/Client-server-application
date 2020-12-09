using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network.Messages
{
    public class NotifyUsers
    {
        #region Properties

        public string Username { get; set; }

        #endregion //Properties

        #region Constructors

        public NotifyUsers(string username)
        {
            Username = username;
        }

        #endregion //Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer()
            {
                Identifier = nameof(NotifyUsers),
                Payload = this
            };

            return container;
        }

        #endregion //Methods
    }
}
