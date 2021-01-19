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

        public DateTime Date { get; }

        public List<string> ActiveUsers { get; }

        #endregion //Properties

        #region Constructors

        public ConnectionStateChangedEventArgs(string username, bool isConnected, DateTime date, List<string> activeUsers = null)
        {
            Username = username;
            IsConnected = isConnected;
            Date = date;
            ActiveUsers = activeUsers;
        }

        public ConnectionStateChangedEventArgs(string username, bool isConnected, DateTime date)
        {
            Username = username;
            IsConnected = isConnected;
            Date = date;
        }

        #endregion //Constructors
    }
}