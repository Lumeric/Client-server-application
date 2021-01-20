using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class MessageReceivedEventArgs
    {
        #region Properties

        public string Username { get; }

        public string Target { get; }

        public string Message { get; }

        public string Groupname { get; }

        public DateTime Date { get; }

        #endregion //Properties

        #region Constructors

        public MessageReceivedEventArgs(string username, string target, string message, string groupname, DateTime date)
        {
            Username = username;
            Target = target;
            Message = message;
            Groupname = groupname;
            Date = date;
        }

        #endregion //Constructors
    }
}
