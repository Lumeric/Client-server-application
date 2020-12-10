using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBModel
{
    public class GeneralChat
    {
        [Key]
        public int MessageID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}
