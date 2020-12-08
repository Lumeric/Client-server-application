using System.Collections.Generic;

namespace Common.Network
{
    public class GroupCreatedEventArgs
    {
        #region Properties

        public string ClientName { get; }

        public List<string> Clients { get; }

        #endregion //Properties

        #region Constructors

        public GroupCreatedEventArgs(string clientName, List<string> users)
        {
            ClientName = clientName;
            Clients = users;
        }

        #endregion //Constructors
    }
}