using System;
using System.Collections.Generic;

namespace Common.Network.Messages
{
    public class ConnectionResponse
    {
        #region Properties

        public ResultCode Result { get; set; }

        public string Reason { get; set; }

        public bool IsSuccess { get; set; }

        public DateTime Date { get; set; }

        public List<string> ActiveUsers { get; set; }

        #endregion //Properties

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
