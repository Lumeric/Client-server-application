using Client.BusinessLogic;
using Client.Views;
using Common.Network;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private Visibility _loginVisibility;
        private Visibility _chatVisibility;
        private Visibility _groupMenuVisibility;
        private Visibility _eventLogVisibility;

        private IEventAggregator _eventAggregator;

        private UserControl _loginView;
        private UserControl _chatView;
        private UserControl _eventLogView;
        private UserControl _groupView;

        private UserControl _selectedView;

        public Visibility LoginVisibility
        {
            get => _loginVisibility;
            set => SetProperty(ref _loginVisibility, value);
        }

        public Visibility ChatVisibility
        {
            get => _chatVisibility;
            set => SetProperty(ref _chatVisibility, value);
        }

        public Visibility GroupMenuVisibility
        {
            get => _groupMenuVisibility;
            set => SetProperty(ref _groupMenuVisibility, value);
        }

        public Visibility EventLogVisibility
        {
            get => _eventLogVisibility;
            set => SetProperty(ref _eventLogVisibility, value);
        }

        public UserControl SelectedView
        {
            get => _selectedView;
            set => SetProperty(ref _selectedView, value);
        }

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _loginView = new LoginView();
            _chatView = new ChatView();
            _eventLogView = new EventLogView();
            _groupView = new GroupView();

            _selectedView = _loginView;

            _eventAggregator.GetEvent<OpenChatEvent>().Subscribe(OpenChatView);
            _eventAggregator.GetEvent<OpenGroupEvent>().Subscribe(OpenGroupView);
            _eventAggregator.GetEvent<OpenEventLogEvent>().Subscribe(OpenEventLogView);
            _eventAggregator.GetEvent<CloseWindowEvent>().Subscribe(OpenLoginView);

            //LoginVisibility = Visibility.Visible;
            //ChatVisibility = Visibility.Collapsed;
            //GroupMenuVisibility = Visibility.Collapsed;
            //EventLogVisibility = Visibility.Collapsed;
        }

        #region Methods

        private void OpenChatView()
        {
            SelectedView = _chatView;
        }

        private void OpenGroupView(ObservableCollection<User> users)
        {
            SelectedView = _groupView;
        }

        private void OpenEventLogView()
        {
            SelectedView = _eventLogView;
        }

        private void OpenLoginView()
        {
            SelectedView = _loginView;
        }

        #endregion //Methods

    }
}
