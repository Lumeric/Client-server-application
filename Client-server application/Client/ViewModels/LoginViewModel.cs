namespace Client.ViewModels
{
    using System;
    using Prism.Commands;
    using Prism.Mvvm;
    using BusinessLogic;
    using System.Text.RegularExpressions;

    public class LoginViewModel : BindableBase
    {
        private const string TARGET_DEFAULT = "all";

        private readonly ILoginController _loginController;

        public LoginViewModel(ILoginController loginController)
        {
            _loginController = loginController ?? throw new ArgumentNullException(nameof(loginController));

            //LoginCommand = new DelegateCommand(ExecuteLoginCommand);

            //ValidUsername = new DelegateCommand(ExecuteValidateUsername);

            Validation = new DelegateCommand(ExecuteValidation);
        }

        //public DelegateCommand LoginCommand { get; }
        //public DelegateCommand ValidUsername { get; }
        public DelegateCommand Validation { get; }

        private void ExecuteLoginCommand()
        {
            _loginController.LoginUser();
        }

        private readonly string regexIP = @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)";
        private readonly string regexUsername = @"[a-z0-9](?:[-_\.]?[a-z0-9])*";
        private string _username = "";
        private string _ip = "";
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

        private void ValidateUsername(string username)
        {
            string errorMessage;

            if (username.Length == 0)
            {
                errorMessage = "Username is required.";
            }
            else if (!Regex.IsMatch(username, regexUsername))
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

            Username = errorMessage;
        }

        //private void ExecuteValidateUsername()
        //{
        //    //_loginController.ValidateUser();
        //    ValidateUsername(Username);
        //}

        private void ValidateIP(string ip)
        {
            string errorMessag;

            if (ip.Length == 0)
            {
                errorMessag = "IP is required.";
            }
            else if (!Regex.IsMatch(ip, regexIP))
            {
                errorMessag = "IP must be valid ip format.\n" +
                               "For example '192.168.1.0'";
            }
            else
            {
                errorMessag = "OK";
            }

            IP = errorMessag;
        }

        //необходимо объединить методы валидации. Также создать один метод который будем вызывать все методы валидации,
        //и биндить его к view. Также подумать над тем, чтобы перенести валидацию в бизнес логику.
        //также подумать над конструкцией if/else, так как по идее можно сделать лучше.

        //общий метод вызова валидации
        private void ExecuteValidation()
        {
            //ValidateUsername(Username);
            ValidateIP(IP);
        }
    }
}
