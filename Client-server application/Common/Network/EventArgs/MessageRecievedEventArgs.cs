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

        public string ClientName { get; }

        public string Message { get; }

        public int Group { get; }

        #endregion Properties

        #region Constructors

        public MessageReceivedEventArgs(string clientName, string message, int group)
        {
            ClientName = clientName;
            Message = message;
            Group = group;
        }

        #endregion Constructors
    }
}
