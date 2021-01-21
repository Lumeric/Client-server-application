using Client.BusinessLogic;
using Common.Network;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class GroupViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IGroupHandler _groupHandler;
        private Visibility _viewVisibility;

        private readonly string groupRegex = "^[A-Za-z0-9]+(?:[ _-][A-Za-z0-9]+){1,12}$";
        private string _groupname;     
        private string _toolTipMessage;     

        private ObservableCollection<User> _users;
        private ObservableCollection<User> _groupList;

        private User _seletedUser;
        private User _deletedUser;

        private bool _isLightTheme = true;
        private bool _isEnable;

        public ObservableCollection<User> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public ObservableCollection<User> GroupList
        {
            get => _groupList;
            set
            {
                SetProperty(ref _groupList, value);
                IsEnable = _groupList.Count != 0;
            }
        }

        public User SelectedUser
        {
            get => _seletedUser;
            set
            {
                SetProperty(ref _seletedUser, value);
                if (!_groupList.Contains(value))
                {
                    _groupList.Add(value);
                    _users.Remove(value);
                }
                if (_groupList.Count != 0)
                {
                    IsEnable = true;
                }
            }
        }

        public User DeletedUser
        {
            get => _deletedUser;
            set
            {
                SetProperty(ref _deletedUser, value);
                if (!_users.Contains(value))
                {
                    _users.Add(value);
                    _groupList.Remove(value);
                }
                if (_groupList.Count == 0)
                {
                    IsEnable = false;
                }
            }
        }

        public string Groupname
        {
            get => _groupname;
            set => SetProperty(ref _groupname, value);
        }

        public string ToolTipMessage
        {
            get => _toolTipMessage;
            set => SetProperty(ref _toolTipMessage, value);
        }

        public Visibility ViewVisibility
        {
            get => _viewVisibility;
            set => SetProperty(ref _viewVisibility, value);
        }

        public bool IsEnable
        {
            get => _isEnable;
            set => SetProperty(ref _isEnable, value);
        }

        public bool IsLightTheme
        {
            get => _isLightTheme;
            set => SetProperty(ref _isLightTheme, value);
        }

        public DelegateCommand CreateGroupCommand { get; set; }

        public DelegateCommand CloseCommand { get; set; }

        public GroupViewModel(IEventAggregator eventAggregator, IGroupHandler groupHandler)
        {
            _eventAggregator = eventAggregator;
            _groupHandler = groupHandler;

            eventAggregator.GetEvent<OpenGroupEvent>().Subscribe(OnOpenGroupChat);
            eventAggregator.GetEvent<CloseWindowEvent>().Subscribe(OnCloseGroupChat);
            eventAggregator.GetEvent<ChangeThemeEvent>().Subscribe(OnChangeStyle);

            CreateGroupCommand = new DelegateCommand(ExecuteCreateGroupCommand);
            CloseCommand = new DelegateCommand(ExecuteCloseCommand);

            _users = new ObservableCollection<User>();
            _groupList = new ObservableCollection<User>();
            _isEnable = false;
            _toolTipMessage = String.Empty;
            _groupname = String.Empty;
            _viewVisibility = Visibility.Collapsed;
        }

        private void ExecuteCreateGroupCommand()
        {
            if (Regex.IsMatch(Groupname, groupRegex))
            {
                _groupHandler.CreateGroupRequest(_groupname, _groupList.Select(g => g.Username).ToList());
                _eventAggregator.GetEvent<OpenChatEvent>().Publish();
                ViewVisibility = Visibility.Collapsed;
            }
            else
            {
                ToolTipMessage = "Invalid groupname.";
            }

            _eventAggregator.GetEvent<OpenChatEvent>().Publish();
            ViewVisibility = Visibility.Collapsed;
        }

        private void ExecuteCloseCommand()
        {
            _eventAggregator.GetEvent<OpenChatEvent>().Publish();
            ViewVisibility = Visibility.Collapsed;
        }

        private void OnOpenGroupChat(ObservableCollection<User> users)
        {
            Users = users;
            ViewVisibility = Visibility.Visible;
        }

        private void OnCloseGroupChat()
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                Users.Clear();
                GroupList.Clear();
            });

            Groupname = String.Empty;
            IsEnable = false;
            ViewVisibility = Visibility.Collapsed;
        }

        private void OnChangeStyle(bool isLightTheme)
        {
            IsLightTheme = isLightTheme;
        }
    }
}
