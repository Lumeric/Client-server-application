namespace Common.Network.Messages
{
    public class ConnectionRequest
    {
        #region Properties

        public string Username { get; set; }

        #endregion //Properties

        #region Constructors

        public ConnectionRequest(string username)
        {
            Username = username;
        }

        #endregion //Constructors

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

        #endregion //Methods
    }
}
