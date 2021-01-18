using Client.BusinessLogic;
using Common.Network;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class GroupViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private Visibility _viewVisibility;

        private bool _isLightTheme = true;

        public Visibility ViewVisibility
        {
            get => _viewVisibility;
            set => SetProperty(ref _viewVisibility, value);
        }

        public bool IsLightTheme
        {
            get => _isLightTheme;
            set => SetProperty(ref _isLightTheme, value);
        }

        public DelegateCommand CreateGroupCommand { get; set; }

        public DelegateCommand CloseCommand { get; set; }

        public GroupViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            eventAggregator.GetEvent<OpenGroupEvent>().Subscribe(OpenGroupChat);
            eventAggregator.GetEvent<CloseWindowEvent>().Subscribe(CloseGroupChat);
            eventAggregator.GetEvent<ChangeThemeEvent>().Subscribe(ChangeStyle);

            CreateGroupCommand = new DelegateCommand(ExecuteCreateGroupCommand);
            CloseCommand = new DelegateCommand(ExecuteCloseCommand);
        }

        private void ExecuteCreateGroupCommand()
        {
            _eventAggregator.GetEvent<OpenChatEvent>().Publish();
            ViewVisibility = Visibility.Collapsed;
        }

        private void ExecuteCloseCommand()
        {
            _eventAggregator.GetEvent<OpenChatEvent>().Publish();
            ViewVisibility = Visibility.Collapsed;
        }

        private void OpenGroupChat(ObservableCollection<User> users)
        {
            ViewVisibility = Visibility.Visible;
        }

        private void CloseGroupChat()
        {
            ViewVisibility = Visibility.Collapsed;
        }

        private void ChangeStyle(bool isLightTheme)
        {
            IsLightTheme = isLightTheme;
        }
    }
}
