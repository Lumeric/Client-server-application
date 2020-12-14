using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBModel
{
    public class GroupTypes
    {
        [Key]
        public int GroupTypeID { get; set; }
        public string GroupType { get; set; }
    }
}
