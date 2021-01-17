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
            containerRegistry.RegisterSingleton<IChatController, ChatController>();
            containerRegistry.RegisterSingleton<IServerResponseController, ServerResponseController>();
            containerRegistry.Register<LoginViewModel>();
            containerRegistry.Register<ChatViewModel>();
            containerRegistry.Register<MainWindowViewModel>();
            containerRegistry.Register<EventLogViewModel>();
            containerRegistry.Register<GroupViewModel>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            BindViewModelToView<LoginViewModel, LoginView>();
            BindViewModelToView<ChatView, ChatViewModel>();
            BindViewModelToView<MainWindow, MainWindowViewModel>();
            BindViewModelToView<EventLogView, EventLogViewModel>();
            BindViewModelToView<GroupView, GroupViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        private void BindViewModelToView<View, ViewModel>()
        {
            ViewModelLocationProvider.Register(typeof(View).ToString(), () => Container.Resolve<ViewModel>());
        }
    }
}
