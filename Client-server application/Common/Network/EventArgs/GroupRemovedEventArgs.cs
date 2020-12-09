using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class GroupRemovedEventArgs
    {
        #region Properties

        public string ClientName { get; }

        public int GroupNumber { get; }

        #endregion //Properties

        #region Constructors

        public GroupRemovedEventArgs(string clientName, int group)
        {
            ClientName = clientName;
            GroupNumber = group;
        }

        #endregion //Constructors
    }
}
