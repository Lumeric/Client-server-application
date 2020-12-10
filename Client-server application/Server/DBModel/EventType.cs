using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBModel
{
    public class EventType
    {
        [Key]
        public int EventID { get; set; }
        public string EventName { get; set; }
    }
}
