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
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            BindViewModelToView<LoginViewModel, LoginView>();
        }

        protected override Window CreateShell()
        {
            var mainView = Container.Resolve<MainWindow>();
            return mainView;
        }

        /*protected override void OnInitialized()
        {
            var login = Container.Resolve<LoginView>();

            base.OnInitialized();
        }*/

        private void BindViewModelToView<ViewModel, View>()
        {
            ViewModelLocationProvider.Register(typeof(View).ToString(), () => Container.Resolve<ViewModel>());
        }
    }
}
