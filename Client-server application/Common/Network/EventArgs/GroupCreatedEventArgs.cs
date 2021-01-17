using System.Collections.Generic;

namespace Common.Network
{
    public class GroupCreatedEventArgs
    {
        #region Properties

        public string Groupname { get; }

        public List<string> Users { get; }

        #endregion //Properties

        #region Constructors

        public GroupCreatedEventArgs(string groupname, List<string> users)
        {
            Groupname = groupname;
            Users = users;
        }

        #endregion //Constructors
    }
}