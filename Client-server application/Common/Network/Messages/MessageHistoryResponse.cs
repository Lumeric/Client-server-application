using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network.Messages
{
    public class MessageHistoryResponse
    {
        #region Properties

        public Dictionary<string, List<Message>> GroupMessages { get; set; }

        #endregion // Properties

        #region Constructors

        public MessageHistoryResponse(Dictionary<string, List<Message>> groupMessages)
        {
            GroupMessages = groupMessages;
        }

        #endregion // Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(MessageHistoryResponse),
                Payload = this
            };

            return container;
        }

        #endregion // Methods
    }
}
