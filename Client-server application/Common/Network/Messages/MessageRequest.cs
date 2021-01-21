using System;

namespace Common.Network.Messages
{
    public class MessageRequest
    {
        #region Properties

        public string Target { get; set; }

        public string Message { get; set; }  

        public string Groupname { get; set; }

        #endregion // Properties

        #region Constructors

        public MessageRequest(string target, string message, string groupname)
        {
            Target = target;
            Message = message;
            Groupname = groupname;
        }

        #endregion // Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(MessageRequest),
                Payload = this
            };

            return container;
        }

        #endregion // Methods
    }
}
