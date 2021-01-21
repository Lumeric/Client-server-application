using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBModel
{
    public class UserList
    {
        #region Properties

        [Key]
        public string Username { get; set; }

        public List<GroupList> GroupList { get; set; }

        #endregion Properties

        #region Constructors

        public UserList()
        {
            GroupList = new List<GroupList>();
        }

        #endregion Constructors
    }
}
