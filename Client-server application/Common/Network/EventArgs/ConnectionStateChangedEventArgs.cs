using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class ConnectionStateChangedEventArgs
    {
        #region Properties

        public string Username { get; }

        public bool IsConnected { get; }

        #endregion //Properties

        #region Constructors

        public ConnectionStateChangedEventArgs(string username, bool isConnected)
        {
            Username = username;
            IsConnected = isConnected;
        }

        #endregion //Constructors
    }
}