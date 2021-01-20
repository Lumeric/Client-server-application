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
    public class EventLogViewModel : BindableBase
    {
        #region Constants
        #endregion //Constants

        #region Fields

        private IEventAggregator _eventAggregator;
        private Visibility _viewVisibility;

        private readonly List<int> _hours = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10, 11, 12, 
                                                            13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

        private readonly List<int> _minutes = new List<int>() { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };

        private int _firstSetHours;
        private int _firstSetMinutes;
        private int _secondSetHours;
        private int _secondSetMinutes;

        private bool _isLightTheme = true;

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

        public List<int> Hours
        {
            get => _hours;
        }

        public List<int> Minutes
        {
            get => _minutes;
        }

        public int FirstSetHours
        {
            get => _firstSetHours;
            set => SetProperty(ref _firstSetHours, value);
        }

        public int FirstSetMinutes
        {
            get => _firstSetMinutes;
            set => SetProperty(ref _firstSetMinutes, value);
        }
        public int SecondSetHours
        {
            get => _secondSetHours;
            set => SetProperty(ref _secondSetHours, value);
        }
        public int SecondSetMinutes
        {
            get => _secondSetMinutes;
            set => SetProperty(ref _secondSetMinutes, value);
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

            _firstSetHours = _hours[0];
            _firstSetMinutes = _minutes[0];
            _secondSetHours = _hours[0];
            _secondSetMinutes = _minutes[0];

            _viewVisibility = Visibility.Collapsed;

            FindCommand = new DelegateCommand(ExecuteFindCommand);
            CloseCommand = new DelegateCommand(ExecuteCloseCommand);
        }

        private void ExecuteFindCommand()
        {
            _eventAggregator.GetEvent<OpenChatEvent>().Publish();
            ViewVisibility = Visibility.Collapsed;
            Console.WriteLine($"{FirstSetHours}:{FirstSetMinutes}");
            Console.WriteLine($"{SecondSetHours}:{SecondSetMinutes}");
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
