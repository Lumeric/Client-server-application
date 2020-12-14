using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBModel
{
    public class GroupMembers
    {
        [Key]
        public int ID { get; set; } // counter
        public int GroupID { get; set; }
        public int UserID { get; set; }
    }
}
