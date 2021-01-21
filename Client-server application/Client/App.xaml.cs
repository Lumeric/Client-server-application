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
    using Common.Network;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILoginHandler, LoginHandler>();
            containerRegistry.RegisterSingleton<IChatHandler, ChatHandler>();
            containerRegistry.RegisterSingleton<IEventLogHandler, EventHandler>();
            containerRegistry.RegisterSingleton<IGroupHandler, GroupHandler>();
            containerRegistry.RegisterSingleton<ITransport, WsClient>();
            containerRegistry.Register<LoginViewModel>();
            containerRegistry.Register<ChatViewModel>();
            containerRegistry.Register<MainWindowViewModel>();
            containerRegistry.Register<EventLogViewModel>();
            containerRegistry.Register<GroupViewModel>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            BindViewModelToView<LoginView, LoginViewModel>();
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
