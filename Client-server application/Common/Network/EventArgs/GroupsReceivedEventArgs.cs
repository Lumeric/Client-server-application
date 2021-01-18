using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class GroupsReceivedEventArgs
    {
        #region Properties

        public Dictionary<string, List<string>> Groups { get; }

        #endregion //Properties

        #region Constructors

        public GroupsReceivedEventArgs(Dictionary<string, List<string>> groups)
        {
            Groups = groups;
        }

        #endregion //Constructors
    }
}
