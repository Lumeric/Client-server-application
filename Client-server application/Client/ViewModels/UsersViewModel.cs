﻿using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Common.Network;
using System.Text;
using System.Threading.Tasks;
using Client.BusinessLogic;

namespace Client.ViewModels
{
    public class UsersViewModel : BindableBase
    {
        private LoginController _loginController;
        private string _username;
        private Guid _id;
        private List<string> _activeUsers;
        private List<string> _inactiveusers;

        public event EventHandler<UserConnectedEventArgs> UserConnected;

        public List<string> InactiveUsers
        {
            get { return _inactiveusers; }
            set { SetProperty(ref _inactiveusers, value); }
        }


        public List<string> ActiveUsers
        {
            get { return _activeUsers; }
            set { SetProperty(ref _activeUsers, value); }
        }

        public string Username 
        { 
            get => _username; 
            set => SetProperty(ref _username, value);
        }

        public UsersViewModel()
        {

            _activeUsers = new List<string>();
            _inactiveusers = new List<string>();

            //test input data
            ActiveUsers.Add("User1");
            ActiveUsers.Add("User2");
            ActiveUsers.Add("User4");
            InactiveUsers.Add("User3");
            InactiveUsers.Add("User5");
            _username = $"You logged as { ActiveUsers.Last<string>() }";
            //end test input data
        }

        //throw to loginViewModel in delegateCommand when chel press button
        //protected void OnUserConnected(UserConnectedEventArgs args)
        //{
        //    UserConnected?.Invoke(this, new UserConnectedEventArgs(_username, _id));
        //}
    }
}