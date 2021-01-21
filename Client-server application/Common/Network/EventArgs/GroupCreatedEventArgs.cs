using System.Collections.Generic;

namespace Common.Network
{
    public class GroupCreatedEventArgs
    {
        #region Properties

        public string Groupname { get; }

        public List<string> UserList { get; }

        #endregion //Properties

        #region Constructors

        public GroupCreatedEventArgs(string groupname, List<string> userList)
        {
            Groupname = groupname;
            UserList = userList;
        }

        #endregion //Constructors
    }
}