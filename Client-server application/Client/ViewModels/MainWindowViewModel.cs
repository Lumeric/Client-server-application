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
        //Fields
        private readonly IEventAggregator _eventAggregator;

        private readonly UserControl _loginView;
        private readonly UserControl _chatView;
        private readonly UserControl _eventLogView;
        private readonly UserControl _groupView;

        private UserControl _selectedView;

        //Properties
        public UserControl SelectedView
        {
            get => _selectedView;
            set => SetProperty(ref _selectedView, value);
        }

        #region Constructors

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
        }

        #endregion // Constructors

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

        #endregion // Methods

    }
}
