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
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class ChatViewModel : BindableBase
    {
        #region Constants

        private const string GENERAL_CHAT = "General";
        private const string EVENT_LOG = "Event Log";

        #endregion //Constants

        #region Fields

        private IEventAggregator _eventAggregator;
        private IChatHandler _chatHandler;
        private Visibility _viewVisibility;
        private string _username;
        private string _address;
        private string _typingText;

        private User _selectedUser;

        private ObservableCollection<User> _users;
        private ObservableCollection<User> _activeUsers;
        private ObservableCollection<User> _inactiveUsers;

        private ObservableCollection<User> _groupList;
        private ObservableCollection<Message> _groupMessages;

        private Dictionary<string, List<Message>> _groups;

        private bool _isLightTheme = true;
        private bool _isGroupMessage;

        #endregion //Fields

        #region Properties

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

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public string TypingText
        {
            get => _typingText;
            set => SetProperty(ref _typingText, value);
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (value == null)
                {
                    SetProperty(ref _selectedUser, _users.FirstOrDefault(u => u.Username == GENERAL_CHAT));
                    return;
                }
                else
                {
                    SetProperty(ref _selectedUser, value);
                }

                SelectedUserChanged(value);
            }
        }
        public ObservableCollection<User> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public ObservableCollection<User> GroupList
        {
            get => _groupList;
            set => SetProperty(ref _groupList, value);
        }

        public ObservableCollection<Message> GroupMessages
        {
            get => _groupMessages;
            set => SetProperty(ref _groupMessages, value);
        }

        public Dictionary<string, List<Message>> Groups
        {
            get => _groups;
            set => SetProperty(ref _groups, value);
        }

        public bool IsLightTheme
        {
            get => _isLightTheme;
            set => SetProperty(ref _isLightTheme, value);
        }

        public bool IsGroupMessage
        {
            get => _isGroupMessage;
            set => SetProperty(ref _isGroupMessage, value);
        }

        public DelegateCommand SendMessageCommand { get; }

        public DelegateCommand CloseChatCommand { get; set; }

        public DelegateCommand OpenGroupCommand { get; set; }

        public DelegateCommand LightThemeCommand { get; set; }

        public DelegateCommand DarkThemeCommand { get; set; }

        public DelegateCommand OpenEventLogCommand { get; set; }

        public DelegateCommand LeaveGroupCommand { get; set; }

        #endregion //Properties

        #region Constructors

        public ChatViewModel(IEventAggregator eventAggregator, IChatHandler chatHandler)
        {
            _eventAggregator = eventAggregator;
            _chatHandler = chatHandler;
            _viewVisibility = Visibility.Collapsed;

            _users = new ObservableCollection<User>();

            _groupList = new ObservableCollection<User>();
            _groupMessages = new ObservableCollection<Message>();
            _groups = new Dictionary<string, List<Message>>();

            eventAggregator.GetEvent<OpenChatEvent>().Subscribe(ChangeVisibility);

            _chatHandler.ConnectionStateChanged += OnConnectionStateChanged;
            _chatHandler.ConnectionReceived += OnConnectionReceived;
            _chatHandler.MessageReceived += OnMessageReceived;
            _chatHandler.UsersReceived += OnUsersReceived;
            _chatHandler.MessageHistoryReceived += OnMessageHistoryReceived;
            _chatHandler.FilteredLogsReceived += OnFilteredLogsReceived;
            _chatHandler.GroupsReceived += OnGroupsReceived;

            SendMessageCommand = new DelegateCommand(ExecuteSendCommand);
            CloseChatCommand = new DelegateCommand(ExecuteCloseChatCommand);
            LightThemeCommand = new DelegateCommand(ExecuteLightThemeCommand);
            DarkThemeCommand = new DelegateCommand(ExecuteDarkThemeCommand);
            OpenEventLogCommand = new DelegateCommand(ExecuteOpenEventLogCommand);
            OpenGroupCommand = new DelegateCommand(ExecuteOpenGroupCommand);
            LeaveGroupCommand = new DelegateCommand(ExecuteLeaveGroupCommand);

            //test
        }

        #endregion //Constructors

        #region Methods

        private void ChangeVisibility()
        {
            ViewVisibility = Visibility.Visible;
        }

        private void ExecuteSendCommand()
        {
            if (String.IsNullOrEmpty(TypingText) || SelectedUser.Username == EVENT_LOG)
            {
                string error = "Invalid operation";
                GroupMessages.Add(new Message(String.Empty, error, true, DateTime.Now));
            }
            else
            {
                if (IsGroupMessage)
                {
                    _chatHandler?.Send(SelectedUser.Username, TypingText, SelectedUser.Username);
                }
                else
                {
                    _chatHandler?.Send(SelectedUser.Username, TypingText, String.Empty);
                }
            }

            TypingText = String.Empty;
        }

        private void ExecuteCloseChatCommand()
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                GroupList.Clear();
                GroupMessages.Clear();
                Users.Clear();
            });

            Groups.Clear();

            ViewVisibility = Visibility.Collapsed;

            _eventAggregator.GetEvent<CloseWindowEvent>().Publish();
            _chatHandler.Disconnect();
        }

        private void ExecuteOpenGroupCommand()
        {
            ObservableCollection<User> users = new ObservableCollection<User>(Users.Select(u => u)
                .Where(u => u.Username != GENERAL_CHAT && u.Username != EVENT_LOG && u.Username != _username));

            _eventAggregator.GetEvent<OpenGroupEvent>().Publish(users);
            ViewVisibility = Visibility.Collapsed;
        }

        private void ExecuteLightThemeCommand()
        {
            IsLightTheme = true;
            _eventAggregator.GetEvent<ChangeThemeEvent>().Publish(IsLightTheme);
        }

        private void ExecuteDarkThemeCommand()
        {
            IsLightTheme = false;
            _eventAggregator.GetEvent<ChangeThemeEvent>().Publish(IsLightTheme);         
        }

        private void ExecuteOpenEventLogCommand()
        {
            _eventAggregator.GetEvent<OpenEventLogEvent>().Publish();
            ViewVisibility = Visibility.Collapsed;
        }

        private void ExecuteLeaveGroupCommand()
        {
            _chatHandler.LeaveGroup(SelectedUser.Username);
            GroupList.Remove(SelectedUser);
        }

        private void OnConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            if (e.IsConnected)
            {
                _username = e.Username;
                ViewVisibility = Visibility.Visible;                

                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    Users.Add(new User(GENERAL_CHAT, true));
                    Users.Add(new User(EVENT_LOG, true));
                    if (e.ActiveUsers != null)
                    {
                        foreach (var user in e.ActiveUsers)
                        {
                            Users.Add(new User(user, true));
                        }
                    }
                });

                SelectedUser = Users.FirstOrDefault(u => u.Username == GENERAL_CHAT);
            }
            else
            {
                ExecuteCloseChatCommand();
            }
        }

        private void OnConnectionReceived(object sender, ConnectionStateChangedEventArgs e)
        {
            User focusedUser = SelectedUser;

            if (!Groups.ContainsKey(e.Username))
            {
                Groups.Add(e.Username, new List<Message>());
            }

            if (e.IsConnected)
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    Users.FirstOrDefault(u => u.Username == e.Username).IsActive = true;
                    Users = new ObservableCollection<User>(Users.OrderByDescending(u => u.IsActive));

                    SelectedUser = focusedUser;
                });

            }
            else
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    Users.FirstOrDefault(u => u.Username == e.Username).IsActive = false;
                    Users = new ObservableCollection<User>(Users.OrderByDescending(u => u.IsActive));

                    SelectedUser = focusedUser;
                });
            }
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.Groupname))
            {
                if (String.IsNullOrEmpty(e.Target) || e.Target == GENERAL_CHAT)
                {
                    Groups[GENERAL_CHAT].Add(new Message(e.Username, e.Message, e.Username == _username, e.Date));
                    if (SelectedUser?.Username == GENERAL_CHAT)
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            GroupMessages = new ObservableCollection<Message>(Groups[GENERAL_CHAT]);
                        });
                    }
                }
                else
                {
                    if (_username == e.Username)
                    {
                        Groups[e.Target].Add(new Message(e.Username, e.Message, true, e.Date));
                        if (SelectedUser.Username == e.Target)
                        {
                            App.Current.Dispatcher.Invoke((Action)delegate
                            {
                                GroupMessages = new ObservableCollection<Message>(Groups[e.Target]);
                            });
                        }
                    }
                    else
                    {
                        Groups[e.Username].Add(new Message(e.Username, e.Message, false, e.Date));
                        if (SelectedUser.Username == e.Username)
                        {
                            App.Current.Dispatcher.Invoke((Action)delegate
                            {
                                GroupMessages = new ObservableCollection<Message>(Groups[e.Username]);
                            });
                        }
                    }
                }
            }
            else
            {
                if (GroupList.FirstOrDefault(g => g.Username == e.Groupname) != default)
                {
                    Groups[e.Groupname].Add(new Message(e.Username, e.Message, e.Username == _username, e.Date));
                    if (SelectedUser.Username == e.Groupname)
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            GroupMessages = new ObservableCollection<Message>(Groups[e.Groupname]);
                        });
                    }
                }
            }
        }

        private void OnUsersReceived(object sender, UsersReceivedEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                foreach (var user in e.Users)
                {
                    if (Users.FirstOrDefault(u => u.Username == user) == default)
                    {
                        Users.Add(new User(user, false));
                    }

                    if (!Groups.ContainsKey(user))
                    {
                        Groups.Add(user, new List<Message>());
                    }
                }
            });
        }

        private void OnMessageHistoryReceived(object sender, MessageHistoryReceivedEventArgs e)
        {
            Groups = e.UserMessages;
            GroupMessages = new ObservableCollection<Message>(Groups[GENERAL_CHAT]);
        }

        private void OnFilteredLogsReceived(object sender, FilteredLogsReceivedEventArgs e)
        {
            Groups[EVENT_LOG] = e.FilteredLogs;
            GroupMessages = new ObservableCollection<Message>(Groups[EVENT_LOG]);
            SelectedUser = Users.FirstOrDefault(u => u.Username == EVENT_LOG);
        }

        private void OnGroupsReceived(object sender, GroupsReceivedEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                foreach (var group in e.Groups)
                {
                    GroupList.Add(new User(group.Key, true));
                }
            });
        }

        private void SelectedUserChanged(User user)
        {
            if (!Groups.ContainsKey(user.Username))
            {
                Groups.Add(user.Username, new List<Message>());
            }

            if (GroupList.FirstOrDefault(g => g.Username == user.Username) != default)
            {
                IsGroupMessage = true;
            }
            else
            {
                IsGroupMessage = false;
            }

            GroupMessages = new ObservableCollection<Message>(Groups[user.Username]);
        }
        #endregion //Methods 
    }
}
