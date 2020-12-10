using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBModel
{
    public class Messages
    {
        [Key]
        public int MessageID { get; set; }
        public int ChatID { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
