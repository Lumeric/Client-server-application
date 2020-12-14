namespace Common.Network
{
    public class UserConnectedToGroupEventArgs
    {
        #region Properties

        public string ClientName { get; }

        public int GroupNumber { get; }

        #endregion //Properties

        #region Constructors

        public UserConnectedToGroupEventArgs(string clientName, int group)
        {
            ClientName = clientName;
            GroupNumber = group;
        }

        #endregion //Constructors
    }
}