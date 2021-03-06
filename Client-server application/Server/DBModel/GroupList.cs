﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBModel
{
    public class GroupList
    {
        #region Properties

        [Key]
        public string Groupname { get; set; }

        public virtual List<UserList> UserList { get; set; }

        #endregion Properties

        #region Constructors

        public GroupList()
        {
            UserList = new List<UserList>();
        }

        #endregion Constructors      
    }
}
