namespace Client.ViewModels
{
    using System;
    using Prism.Commands;
    using Prism.Mvvm;
    using BusinessLogic;
    using System.Text.RegularExpressions;
    using System.Collections.ObjectModel;
    using System.Windows;
    using Prism.Events;
    using Prism.Common;
    using System.ComponentModel;
    using System.Collections.Generic;
    using Common.Network;
    using System.Linq;

    public class LoginViewModel : BindableBase, IDataErrorInfo, IViewModel
    {
        #region Constants

        private const int MIN_USERNAME_LENGTH = 6;
        private const int MAX_USERNAME_LENGTH = 20;

        #endregion //Constants

        #region Events



        #endregion //Events

        #region Fields

        private IEventAggregator _eventAggregator;
        private ILoginController _loginController;
        private static readonly string regexIP = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        private static readonly string regexUsername = @"^[A-Za-z0-9]+(?:[ _-][A-Za-z0-9]+)*$";
        private string _ip = "192.168.1.7";
        private string _port = "65000";
        private string _username = "ValeraVolodya";
        private bool _isConnected;
        private List<string> _sockets;
        private string _selectedSocket;
        private string _helpText;
        private bool _isLightTheme = true;
        private Visibility _viewVisibility;

        #endregion //Fields

        #region Properties

        public string Error { get { return null; } }

        //validation module
        public string this[string parameter]
        {
            get
            {
                string error = null;

                switch (parameter)
                {
                    case "Username":
                        Regex regex = new Regex(regexUsername); 
                        Match match = regex.Match(Username);

                        if (string.IsNullOrWhiteSpace(Username))
                        {
                            error = "Username is required.";
                            IsValidUsername = false;
                        }
                        else if (!match.Success)
                        {
                            error = "Username must be valid username format and contains only alphabetic symbols and numbers.\n" +
                                       "For example 'Cyberprank2020'";
                            IsValidUsername = false;
                        }
                        else if (Username.Length < MIN_USERNAME_LENGTH || Username.Length > MAX_USERNAME_LENGTH)
                        {
                            error = "Username length nust be no less than 6 symbols and no more than 20 symbols.";
                            IsValidUsername = false;
                        }
                        else
                        {
                            IsValidUsername = true;
                        }

                        break;

                    case "IP":
                        regex = new Regex(regexIP);
                        match = regex.Match(IP);

                        if (string.IsNullOrWhiteSpace(IP))
                        {
                            error = "IP is required";
                            IsValidIP = false;
                        }
                        else if (!match.Success)
                        {
                            error = "IP must be valid ip format. For example '192.168.1.0'";
                            IsValidIP = false;
                        }
                        else
                        {
                            IsValidIP = true;
                        }

                        break;

                    case "Port":
                        if (string.IsNullOrWhiteSpace(Port))
                         {
                            error = "Port is required";
                            IsValidPort = false;
                        }
                        else if (!Port.All(char.IsDigit))
                        {
                            error = "Port must be valid";
                            IsValidPort = false;
                        }
                        else
                        {
                            IsValidPort = true;
                        }

                        break;
                }

                if (Errors.ContainsKey(parameter))
                    Errors[parameter] = error;
                else if (error != null)
                    Errors.Add(parameter, error);

                RaisePropertyChanged(nameof(Errors));
                return error;
            }
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string IP
        {
            get => _ip;
            set => SetProperty(ref _ip, value);
        }

        public string Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        public bool IsValidUsername { get; private set; }

        public bool IsValidIP { get; private set; }

        public bool IsValidPort { get; private set; }

        public Dictionary<string, string> Errors { get; set; }

        public List<string> Sockets
        {
            get { return _sockets; }
            set { _sockets = value; }
        }

        public string SelectedSocket
        {
            get { return _selectedSocket; }
            set { _selectedSocket = value; }
        }

        public Visibility ViewVisibility
        {
            get => _viewVisibility;
            set => SetProperty(ref _viewVisibility, value);
        }

        public string HelpText
        {
            get => _helpText;
            set => SetProperty(ref _helpText, value);
        }

        public bool IsLightTheme
        {
            get => _isLightTheme;
            set => SetProperty(ref _isLightTheme, value);
        }
        public DelegateCommand ConnectCommand { get; }

        public DelegateCommand LoginCommand { get; }

        #endregion //Properties

        #region Constructors

        public LoginViewModel(IEventAggregator eventAggregator, ILoginController loginController)
        {
            _eventAggregator = eventAggregator;
            _loginController = loginController;
            Errors = new Dictionary<string, string>();
            _viewVisibility = Visibility.Visible;
            _helpText = "Enter address and port.";
            _isConnected = true; //false

            _sockets = new List<string>();
            _sockets.Add(TransportTypes.WebSocket.ToString());
            _sockets.Add(TransportTypes.TcpSocket.ToString());
            _selectedSocket = _sockets[0];

            _eventAggregator.GetEvent<ChangeThemeEvent>().Subscribe(OnChangeTheme);

            ConnectCommand = new DelegateCommand(ExecuteConnectCommand, CanExecuteConnectCommand).ObservesProperty(() => IP)
                .ObservesProperty(() => Port)
                .ObservesProperty(() => Username);
            LoginCommand = new DelegateCommand(ExecuteLoginCommand, CanExecuteLoginCommand).ObservesProperty(() => IsConnected);

            _loginController.ConnectionStateChanged += OnConnectionStateChanged;
            _loginController.ErrorReceived += OnErrorReceived;
        }

        #endregion //Constructors 

        #region Methods

        private bool CanExecuteConnectCommand()
        {
            return IsValidIP && IsValidPort && IsValidUsername;
        }

        private void ExecuteConnectCommand()
        {
            try
            {
                _loginController.ConnectUser(IP, Port);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExecuteLoginCommand()
        {
            //_loginController.LoginUser(SelectedSocket);
            _eventAggregator.GetEvent<OpenChatEvent>().Publish();
            ViewVisibility = Visibility.Collapsed;
        }

        private bool CanExecuteLoginCommand()
        {
            return IsConnected;
        }

        private void OnConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            if (e.IsConnected)  
                if (string.IsNullOrEmpty(e.Username))
                {
                    IsConnected = true;
                    HelpText = "Enter username.";
                }
        }

        private void OnErrorReceived(object sender, ErrorReceivedEventArgs e)
        {
            HelpText = $"{e.Message}/{e.ErrorType}";
        }

        private void OnChangeTheme(bool isLightTheme)
        {
            IsLightTheme = isLightTheme;
        }

        #endregion //Methods         

    }
}
