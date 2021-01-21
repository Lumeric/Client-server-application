using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network.Messages
{
    public class ConnectionBroadcast
    {
        #region Properties

        public string Username { get; set; }

        public bool IsConnected { get; set; }

        public DateTime Date { get; set; }

        #endregion Properties

        #region Constructors

        public ConnectionBroadcast(string username, bool isConnected, DateTime date)
        {
            Username = username;
            IsConnected = isConnected;
            Date = date;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(ConnectionBroadcast),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
