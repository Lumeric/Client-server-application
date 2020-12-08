namespace Common.Network
{
    public class UserDisconnectedEventArgs
    {
        #region Properties

        public string ClientName { get; }

        #endregion //Properties

        #region Constructors

        public UserDisconnectedEventArgs(string clientName)
        {
            ClientName = clientName;
        }

        #endregion //Constructors
    }
}