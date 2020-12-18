using Client.BusinessLogic;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class ChatViewModel : BindableBase
    {
        //Fields
        private Visibility _viewVisibility;
        private string _username;
        private string _serverIP;
        private List<string> _activeUsers;
        private List<string> _inactiveUsers;

        //Properties
        public Visibility ViewVisibility
        {
            get => _viewVisibility;
            set => SetProperty(ref _viewVisibility, value);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string ServerIP
        {
            get => _serverIP;
            set => SetProperty(ref _serverIP, value);
        }

        public List<string> ActiveUsers
        {
            get => _activeUsers;
            set => SetProperty(ref _activeUsers, value);
        }
        public List<string> InactiveUsers
        {
            get => _inactiveUsers;
            set => SetProperty(ref _inactiveUsers, value);
        }

        public ObservableCollection<GroupListViewModel> PrivateGroups { get; set; }
        //ctors
        public ChatViewModel(IEventAggregator eventAggregator)
        {
            _viewVisibility = Visibility.Collapsed;
            _username = $"Username: " + "Valera";
            _serverIP = $"Server IP " + "192.168.1.2";
            eventAggregator.GetEvent<UserValidatedEvent>().Subscribe(ChangeVisibility);
        }

        //Methods
        private void ChangeVisibility(Visibility obj)
        {
            ViewVisibility = obj;
        }
    }
}
