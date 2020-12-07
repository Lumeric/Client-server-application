namespace Client
{
    using System.Windows;

    using BusinessLogic;

    using Prism.Ioc;
    using Prism.Mvvm;
    using Prism.Unity;

    using Unity;

    using Views;

    using ViewModels;
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILoginController, LoginController>();
            containerRegistry.Register<LoginViewModel>();
            containerRegistry.Register<ChatViewModel>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            BindViewModelToView<LoginViewModel, LoginView>();
            BindViewModelToView<ChatView, ChatViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        private void BindViewModelToView<ViewModel, View>()
        {
            ViewModelLocationProvider.Register(typeof(View).ToString(), () => Container.Resolve<ViewModel>());
        }
    }
}
