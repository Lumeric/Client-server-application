using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class User
    {
        #region Properties

        public string Username { get; set; }

        public bool IsActive { get; set; }

        #endregion //Properties

        #region Constructors

        public User(string username, bool isActive)
        {
            Username = username;
            IsActive = isActive;
        }

        #endregion //Constructors
    }
}
