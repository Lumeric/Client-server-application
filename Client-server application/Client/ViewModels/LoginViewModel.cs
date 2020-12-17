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

    public class LoginViewModel : BindableBase, IDataErrorInfo
    {
        #region Constants

        private const int MIN_USERNAME_LENGTH = 6;
        private const int MAX_USERNAME_LENGTH = 20;

        #endregion //Constants

        #region Events



        #endregion //Events

        #region Fields

        private IEventAggregator _eventAggregator;
        private readonly ILoginController _loginController;
        private static readonly string regexIP = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        private static readonly string regexUsername = @"^[A-Za-z0-9]+(?:[ _-][A-Za-z0-9]+)*$";
        private string _port;
        private string _username = "";
        private string _ip = "";
        private bool _isValidated;
        private ObservableCollection<string> _sockets;
        private string _selectedSocket;
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
                                       "For example 'Cyberpunk2020'";
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
                }

                if (Errors.ContainsKey(parameter))
                    Errors[parameter] = error;
                else if (error != null)
                    Errors.Add(parameter, error);

                RaisePropertyChanged(nameof(Errors));
                OnValidated();
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

        public bool IsValidUsername { get; private set; }

        public bool IsValidIP { get; private set; }

        public Dictionary<string, string> Errors { get; set; }

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

        public DelegateCommand LoginCommand { get; }
        public Visibility ViewVisibility 
        { 
            get => _viewVisibility;
            set => SetProperty(ref _viewVisibility, value); 
        }

        #endregion //Properties

        #region Constructors

        public LoginViewModel(IEventAggregator eventAggregator, ILoginController loginController)
        {
            _eventAggregator = eventAggregator;
            _loginController = loginController ?? throw new ArgumentNullException(nameof(loginController));
            Errors  = new Dictionary<string, string>();
            _viewVisibility = Visibility.Visible;

            _sockets = new ObservableCollection<string>();
            _sockets.Add("WebSocket");
            _sockets.Add("TcpSocket");
            _selectedSocket = _sockets[0];

            LoginCommand = new DelegateCommand(ExecuteLoginCommand, CanExecuteLoginCommand).ObservesProperty(() => Port).ObservesProperty(() => IsValidated);
        }

        #endregion //Constructors

        #region Methods

        private void ExecuteLoginCommand()
        {
            //_loginController.LoginUser();
            _eventAggregator.GetEvent<UserValidatedEvent>().Publish(ViewVisibility);
            ViewVisibility = Visibility.Collapsed;
        }

        private bool CanExecuteLoginCommand()
        {
            //return !String.IsNullOrWhiteSpace(Port) && IsValidated;
            return true; // temporal code for faster testing
        }

        private void OnValidated()
        {
            if (IsValidUsername && IsValidIP)
                IsValidated = true;
            else IsValidated = false;

        }

        #endregion //Methods         

    }
}
