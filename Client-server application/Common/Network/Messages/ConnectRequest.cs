namespace Common.Network.Messages
{
    public class ConnectRequest
    {
        #region Properties

        public string Username { get; set; }

        #endregion //Properties

        #region Constructors

        public ConnectRequest(string username)
        {
            Username = username;
        }

        #endregion //Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(ConnectRequest),
                Payload = this
            };

            return container;
        }

        #endregion //Methods
    }
}
