using System;
using System.Collections.Generic;

namespace Common.Network.Messages
{
    public class ConnectResponse
    {
        #region Properties

        public ResultCode Result { get; set; }

        public string Reason { get; set; }

        public bool IsSuccess { get; set; }

        public DateTime TIme { get; set; }

        public List<string> ActiveUsers { get; set; }

        #endregion //Properties

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
