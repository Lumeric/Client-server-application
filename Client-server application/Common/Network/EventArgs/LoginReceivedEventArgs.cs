using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class LoginReceivedEventArgs
    {
        #region Properties

        public string Login { get; }

        public bool IsConnected { get; }

        public DateTime Date { get; }

        #endregion Properties

        #region Constructors

        public LoginReceivedEventArgs(string login, bool isConnected, DateTime date)
        {
            Login = login;
            IsConnected = isConnected;
            Date = date;
        }

        #endregion Constructors
    }
}
