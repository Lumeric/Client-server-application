namespace Common.Network.Messages
{
    public class ConnectionRequest
    {
        #region Properties

        public string Username { get; set; }

        #endregion Properties

        #region Constructors

        public ConnectionRequest(string login)
        {
            Username = login;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(ConnectionRequest),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
