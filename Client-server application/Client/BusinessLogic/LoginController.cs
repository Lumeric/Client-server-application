using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ViewModels;
using Common.Network;

namespace Client.BusinessLogic
{
    public class LoginController : ILoginController
    {
        private ITransport _currentTransport;
        public void LoginUser(string socket)
        {
            //try
            //{
            //    _currentTransport = TransportFactory.Create((TransportType)sockets)
            //}
        }

        //public LoginController(UsersViewModel uvm)
        //{
        //    //uvm.UserConnected += SomeVoid;

        //}

        //public void SomeVoid(object sender, UserConnectedEventArgs args)
        //{
            
        //}
    }
}
