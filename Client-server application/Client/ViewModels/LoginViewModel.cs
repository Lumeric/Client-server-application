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

            LoginCommand = new DelegateCommand(ExecuteLoginCommand);

            ValidUsername = new DelegateCommand(ValidatingUsername);
        }

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand ValidUsername { get; }

        private void ExecuteLoginCommand()
        {
            _loginController.LoginUser();
        }
        

        private readonly string regexUsername = @"\w";
        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public bool ValidateUsername(string username, out string errorMessage)
        {
            if (username.Length == 0)
            {
                errorMessage = "Username is required.";
                return false;
            }

            if (Regex.IsMatch(username, regexUsername))
            {
                errorMessage = "";
                return true;
            }

            errorMessage = "Username must be valid username  format.\n" +
               "For example 'Hasagi' ";
            return false;
        }

        public void ValidatingUsername()
        {
            string errorMsg;

            if (ValidateUsername(_username, out errorMsg))
            {
                Username = "Dummy";
            }
        }
    }
}
