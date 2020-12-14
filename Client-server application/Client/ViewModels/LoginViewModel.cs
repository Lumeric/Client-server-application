namespace Client.ViewModels
{
    using System;
    using Prism.Commands;
    using Prism.Mvvm;
    using BusinessLogic;
    using System.Text.RegularExpressions;
    using System.Collections.ObjectModel;

    public class LoginViewModel : BindableBase, IViewModel
    {
        #region Constants



        #endregion //Constants

        #region Events



        #endregion //Events

        #region Fields

        private readonly ILoginController _loginController;
        private static readonly string regexIP = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        private static readonly string regexUsername = @"^(?!.*[0-9])[a-z](?:[\w]*|[a-z\d\.]*|[a-z\d-]*)[a-z0-9]$";
        private string _port;
        private string _username = "";
        private string _ip = "";
        private string _error = "";
        private ObservableCollection<string> _sockets;
        private string _selectedSocket;

        #endregion //Fields

        #region Properties

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

        public string Error 
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }

        public ObservableCollection<string> Sockets
        {
            get { return _sockets; }
            set { _sockets = value; }
        }

        public string SelectedSocket
        {
            get { return _selectedSocket; }
            set { _selectedSocket = value; }
        }

        public DelegateCommand Validation { get; }

        #endregion //Properties

        #region Constructors

        public LoginViewModel(ILoginController loginController)
        {
            _loginController = loginController ?? throw new ArgumentNullException(nameof(loginController));

            Sockets = new ObservableCollection<string>();
            Sockets.Add("WebSocket");
            Sockets.Add("TcpSocket");
            SelectedSocket = Sockets[0];

            //LoginCommand = new DelegateCommand(ExecuteLoginCommand);

            Validation = new DelegateCommand(ExecuteValidation, CanExecuteValidation).ObservesProperty(() => IP).ObservesProperty(() => Port).ObservesProperty(() => Username);
        }

        public LoginViewModel()
        {
            //empty constructor
            Sockets = new ObservableCollection<string>();
            Sockets.Add("WebSocket");
            Sockets.Add("TcpSocket");
            SelectedSocket = Sockets[0];

            //LoginCommand = new DelegateCommand(ExecuteLoginCommand);

            Validation = new DelegateCommand(ExecuteValidation, CanExecuteValidation).ObservesProperty(() => IP).ObservesProperty(() => Port).ObservesProperty(() => Username);
        }

        #endregion //Constructors

        #region Methods

        //private void ExecuteLoginCommand()
        //{
        //    _loginController.LoginUser();
        //}

        private void ValidateUsername(string username)
        {
            string errorMessage;
            Regex regex = new Regex(regexUsername);
            Match match = regex.Match(username);

            if (username.Length == 0)
            {
                errorMessage = "Username is required.";
            }
            else if (!match.Success)
            {
                errorMessage = "Username must be valid username format and contains only alphabetic symbols and numbers.\n" +
                           "For example 'Cyberpunk2020'";
            }
            else if (username.Length < 6 || username.Length > 20)
            {
                errorMessage = "Username length nust be no less than 6 symbols and no more than 20 symbols.";
            }
            else
            {
                errorMessage = "OK";
            }

            Error = errorMessage;
        }

        private void ValidateIP(string ip)
        {
            string errorMessage;
            Regex regex = new Regex(regexIP);
            Match match = regex.Match(ip);

            if (ip.Length == 0)
            {
                errorMessage = "IP is required.";
            }
            else if (!match.Success)
            {
                errorMessage = "IP must be valid ip format. For example '192.168.1.0'";
            }
            else
            {
                errorMessage = "OK";
            }

            IP = errorMessage;
        }
        private bool CanExecuteValidation()
        {
            return !String.IsNullOrWhiteSpace(IP) && !String.IsNullOrWhiteSpace(Port) && !String.IsNullOrWhiteSpace(Username);
        }

        //общий метод вызова валидации
        private void ExecuteValidation()
        {
                ValidateUsername(Username);
                ValidateIP(IP);
        }

        #endregion //Methods         

    }
}
