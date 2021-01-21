using Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public interface ILoginHandler
    {
        event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        event EventHandler<ErrorReceivedEventArgs> ErrorReceived;

        void ConnectUser(string address, string port);

        void DisconnectUser();

        void LoginUser(string username);

    }
}
