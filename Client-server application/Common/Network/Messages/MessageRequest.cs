namespace Common.Network.Messages
{
    public class MessageRequest
    {
        #region Properties

        public string Login { get; set; }

        public string Message { get; set; }

        public int Group { get; set; }

        #endregion Properties

        #region Constructors

        public MessageRequest(string login, string message, int group)
        {
            Login = login;
            Message = message;
            Group = group;
        }

        #endregion Constructors

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

        #endregion Methods
    }
}
