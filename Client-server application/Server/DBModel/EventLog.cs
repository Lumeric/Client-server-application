using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBModel
{
    public class EventLog
    {
        [Key]
        public int ID { get; set; } // counter
        public string EventText { get; set; }
        public int EventID { get; set; }
        public DateTime Time { get; set; }
    }
}
