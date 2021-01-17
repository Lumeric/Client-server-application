using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class UsersReceivedEventArgs
    {
        #region Properties

        public List<string> Users { get; }

        #endregion //Properties

        #region Constructors

        public UsersReceivedEventArgs(List<string> users)
        {
            Users = users;
        }

        #endregion //Constructors
    }
}
