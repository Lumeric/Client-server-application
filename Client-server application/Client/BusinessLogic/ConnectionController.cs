using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public class ConnectionController
    {
        private string _address;
        private string _port;
        private string _username;

        public ConnectionController()
        {
            
        }

        public void Connect(string address, string port)
        {
            _address = address;
            _port = port;
        }
    }
}
