using System;

namespace Common.Network.Messages
{
    public class MessageRequest
    {
        #region Properties

        public string Username { get; set; }

        public string Message { get; set; }

        public int Group { get; set; }

        public DateTime Date { get; set; }

        #endregion //Properties

        #region Constructors

        public MessageRequest(string username, string message, int group, DateTime date)
        {
            Username = username;
            Message = message;
            Group = group;
            Date = date;
        }

        #endregion //Constructors

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

        #endregion //Methods
    }
}
