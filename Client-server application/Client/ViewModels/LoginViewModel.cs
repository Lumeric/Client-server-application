namespace Client.ViewModels
{
    using System;
    using Prism.Commands;
    using Prism.Mvvm;
    using BusinessLogic;

    public class LoginViewModel : BindableBase
    {
        private const string TARGET_DEFAULT = "all";

        private readonly ILoginController _loginController;

        public LoginViewModel(ILoginController loginController)
        {
            _loginController = loginController ?? throw new ArgumentNullException(nameof(loginController));

            LoginCommand = new DelegateCommand(ExecuteLoginCommand);
        }

        public DelegateCommand LoginCommand { get; }
        
        private void ExecuteLoginCommand()
        {
            _loginController.LoginUser();
        }
    }
}
