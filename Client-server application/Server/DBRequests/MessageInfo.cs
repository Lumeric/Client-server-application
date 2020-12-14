using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RequestsToDB
{
    public class MessageInfo
    {
        public int ChatID { get; set; }
        public string Username { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }
    }
}
