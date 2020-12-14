using System.Collections.Generic;

namespace Common.Network
{
    public class GroupCreatedEventArgs
    {
        #region Properties

        public string Username { get; }

        public List<string> Users { get; }

        #endregion //Properties

        #region Constructors

        public GroupCreatedEventArgs(string clientName, List<string> users)
        {
            Username = clientName;
            Users = users;
        }

        #endregion //Constructors
    }
}