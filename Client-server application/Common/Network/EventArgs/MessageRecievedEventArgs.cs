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

        public string Message { get; }

        public int Group { get; }

        public DateTime Date { get; }

        #endregion //Properties

        #region Constructors

        public MessageReceivedEventArgs(string username, string message, int group, DateTime date)
        {
            Username = username;
            Message = message;
            Group = group;
            Date = date;
        }

        #endregion //Constructors
    }
}
