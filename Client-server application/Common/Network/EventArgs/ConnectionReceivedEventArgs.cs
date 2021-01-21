using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class ConnectionReceivedEventArgs
    {
        #region Properties

        public string Username { get; }

        public bool IsConnected { get; }

        public DateTime Date { get; }

        #endregion // Properties

        #region Constructors

        public ConnectionReceivedEventArgs(string username, bool isConnected, DateTime date)
        {
            Username = username;
            IsConnected = isConnected;
            Date = date;
        }

        #endregion // Constructors
    }
}
