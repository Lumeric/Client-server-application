using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class GroupLeavedEventArgs
    {
        #region Properties

        public string Username { get; }

        public string Groupname { get; }

        #endregion // Properties

        #region Constructors

        public GroupLeavedEventArgs(string clientName, string groupname)
        {
            Username = clientName;
            Groupname = groupname;
        }

        #endregion // Constructors
    }
}
