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

        private const string GeneralChat = "General";
        private const string EventLog = "Event Log";

        #endregion //Constants

        #region Fields

        private IEventAggregator _eventAggregator;
        private IChatHandler _chatController;
        private Visibility _viewVisibility;
        private string _username;
        private string _userIP;
        private string _typingText;

        private User _selectedUser;

        private ObservableCollection<User> _users;
        private ObservableCollection<User> _activeUsers;
        private ObservableCollection<User> _inactiveUsers;
        private ObservableCollection<Message> _groupMessages;

        private Dictionary<string, List<Message>> _groups;

        private bool _isLightTheme = true;

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

        public string UserIP
        {
            get => _userIP;
            set => SetProperty(ref _userIP, value);
        }

        public string TypingText
        {
            get => _typingText;
            set => SetProperty(ref _typingText, value);
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

        public ObservableCollection<User> ActiveUsers
        {
            get => _activeUsers;
            set => SetProperty(ref _activeUsers, value);
        }
        public ObservableCollection<User> InactiveUsers
        {
            get => _inactiveUsers;
            set => SetProperty(ref _inactiveUsers, value);
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

        public DelegateCommand OpenGroupCommand { get; set; }

        public DelegateCommand LightThemeCommand { get; set; }

        public DelegateCommand DarkThemeCommand { get; set; }

        public DelegateCommand CloseChatCommand { get; set; }

        public DelegateCommand OpenEventLogCommand { get; set; }

        #endregion //Properties

        #region Constructors

        public ChatViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _viewVisibility = Visibility.Collapsed;
            _username = "Valera";
            _userIP = "123.123.123.0";

            _users = new ObservableCollection<User>();
            _activeUsers = new ObservableCollection<User>();
            _inactiveUsers = new ObservableCollection<User>();
            _groupMessages = new ObservableCollection<Message>();
            _groups = new Dictionary<string, List<Message>>();

            eventAggregator.GetEvent<OpenChatEvent>().Subscribe(ChangeVisibility);

            LightThemeCommand = new DelegateCommand(ExecuteLightThemeCommand);
            DarkThemeCommand = new DelegateCommand(ExecuteDarkThemeCommand);
            CloseChatCommand = new DelegateCommand(ExecuteCloseChatCommand);
            OpenEventLogCommand = new DelegateCommand(ExecuteOpenEventLogCommand);
            OpenGroupCommand = new DelegateCommand(ExecuteOpenGroupCommand);


            //test
            _activeUsers.Add(new User("Valera", true));
            _activeUsers.Add(new User("Sanya", true));
            _activeUsers.Add(new User("Cepera", true));

            _inactiveUsers.Add(new User("Maks", false));
            _inactiveUsers.Add(new User("Oniq", false));

            _groupMessages.Add(new Message("Sanya", "Privet kotikam", true, DateTime.Now));
            _groupMessages.Add(new Message("Cepera", "Privet chelovekam", false, DateTime.Now));
        }

        #endregion //Constructors

        #region Methods

        private void ChangeVisibility()
        {
            ViewVisibility = Visibility.Visible;
        }

        private void ExecuteOpenGroupCommand()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();  
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

        private void ExecuteCloseChatCommand()
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                ActiveUsers.Clear();
                InactiveUsers.Clear();
                GroupMessages.Clear();
                Groups.Clear();
            });
         
            ViewVisibility = Visibility.Collapsed;

            _eventAggregator.GetEvent<CloseWindowEvent>().Publish();
        }

        private void ExecuteOpenEventLogCommand()
        {
            _eventAggregator.GetEvent<OpenEventLogEvent>().Publish();
            ViewVisibility = Visibility.Collapsed;
        }
        #endregion //Methods 
    }
}
