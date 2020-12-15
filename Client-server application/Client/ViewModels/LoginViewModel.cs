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

    public class LoginViewModel : BindableBase, IViewModel, IDataErrorInfo
    {
        #region Constants



        #endregion //Constants

        #region Events



        #endregion //Events

        #region Fields

        private IEventAggregator _eventAggregator;
        private readonly ILoginController _loginController;
        private static readonly string regexIP = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        private static readonly string regexUsername = @"^(?!.*[0-9])[a-z](?:[\w]*|[a-z\d\.]*|[a-z\d-]*)[a-z0-9]$";
        private string _port;
        private string _username = "";
        private string _ip = "";
        private bool _isValidated;
        private ObservableCollection<string> _sockets;
        private string _selectedSocket;
        private Visibility _visibility;

        #endregion //Fields

        #region Properties

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
                        }
                        else if (!match.Success)
                        {
                            error = "Username must be valid username format and contains only alphabetic symbols and numbers.\n" +
                                       "For example 'Cyberpunk2020'";
                        }
                        else if (Username.Length < 6 || Username.Length > 20)
                        {
                            error = "Username length nust be no less than 6 symbols and no more than 20 symbols.";                        
                        }
                        break;

                    case "IP":
                        regex = new Regex(regexIP);
                        match = regex.Match(IP);

                        if (string.IsNullOrWhiteSpace(IP))
                        {
                            error = "IP is required";
                        }
                        else if (!match.Success)
                        {
                            error = "IP must be valid ip format. For example '192.168.1.0'";
                        }
                        break;
                }

                if (Errors.ContainsKey(parameter))
                    Errors[parameter] = error;
                else if (error != null)
                    Errors.Add(parameter, error);
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

        public bool IsValidated
        {
            get => _isValidated;
            set => SetProperty(ref _isValidated, value);
        }

        private Dictionary<string, string> Errors { get; set; }

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
        public Visibility Visibility 
        { 
            get => _visibility;
            set => SetProperty(ref _visibility, value); 
        }

        public string Error { get { return null; } }

        #endregion //Properties

        #region Constructors

        public LoginViewModel(IEventAggregator eventAggregator, ILoginController loginController)
        {
            _eventAggregator = eventAggregator;
            _loginController = loginController ?? throw new ArgumentNullException(nameof(loginController));
            Errors = new Dictionary<string, string>();
            _isValidated = false;

            _sockets = new ObservableCollection<string>();
            _sockets.Add("WebSocket");
            _sockets.Add("TcpSocket");
            _selectedSocket = _sockets[0];

            //LoginCommand = new DelegateCommand(ExecuteLoginCommand);

            Validation = new DelegateCommand(ExecuteValidation, CanExecuteValidation).ObservesProperty(() => IP).ObservesProperty(() => Port).ObservesProperty(() => Username);
        }

        #endregion //Constructors

        #region Methods

        //private void ExecuteLoginCommand()
        //{
        //    _loginController.LoginUser();
        //}

        private bool CanExecuteValidation()
        {
            return !String.IsNullOrWhiteSpace(IP) && !String.IsNullOrWhiteSpace(Port) && !String.IsNullOrWhiteSpace(Username);
        }

        //общий метод вызова валидации
        private void ExecuteValidation()
        {
            //ValidateUsername(Username);
            //ValidateIP(IP);
            _eventAggregator.GetEvent<UserValidatedEvent>().Publish(IsValidated);
        }

        private void OnValidated()
        {
            if (IsValidated)
            {
            }
        }

        #endregion //Methods         

    }
}
