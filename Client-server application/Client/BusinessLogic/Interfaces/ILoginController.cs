using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public interface ILoginController
    {
        void ConnectUser(string ip, string port);
        void LoginUser(string username);

    }
}
