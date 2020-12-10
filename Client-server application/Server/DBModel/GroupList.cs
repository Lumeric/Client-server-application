using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBModel
{
    public class GroupList
    {
        [Key]
        public int GroupID { get; set; }
        public string Groupname { get; set; }
        public int GroupTypeID { get; set; }
        public int UserID { get; set; } // group creator        
    }
}
