using Client.BusinessLogic;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class EventLogViewModel : BindableBase, IViewModel
    {
        #region Fields

        private IEventAggregator _eventAggregator;
        private Visibility _viewVisibility;

        private bool _isLightTheme;

        #endregion //Fields

        #region Properties

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

        public DelegateCommand FindCommand { get; set; }

        public DelegateCommand CloseCommand { get; set; }

        #endregion //Properties

        public EventLogViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<OpenEventLogEvent>().Subscribe(OpenEventLog);
            _eventAggregator.GetEvent<CloseWindowEvent>().Subscribe(CloseEventLog);
            _eventAggregator.GetEvent<ChangeThemeEvent>().Subscribe(ChangeThemeEventLog);

            _viewVisibility = Visibility.Collapsed;

            FindCommand = new DelegateCommand(ExecuteFindCommand);
            CloseCommand = new DelegateCommand(ExecuteCloseCommand);
        }

        private void ExecuteFindCommand()
        {
            _eventAggregator.GetEvent<OpenChatEvent>().Publish();
            ViewVisibility = Visibility.Collapsed;
        }

        private void ExecuteCloseCommand()
        {
            _eventAggregator.GetEvent<OpenChatEvent>().Publish();
            ViewVisibility = Visibility.Collapsed;
        }

        private void OpenEventLog()
        {
            ViewVisibility = Visibility.Visible;
        }

        private void CloseEventLog()
        {
            //more logic
            ViewVisibility = Visibility.Collapsed;
        }

        private void ChangeThemeEventLog(bool obj)
        {
            IsLightTheme = obj;
        }
    }
}
