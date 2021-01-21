using Client.BusinessLogic;
using Common.Network;
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
        private IEventLogHandler _eventLogHandler;
        private Visibility _viewVisibility;

        private readonly List<int> _hours = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10, 11, 12, 
                                                            13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

        private readonly List<int> _minutes = new List<int>() { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };

        private DateTime _firstDate;
        private DateTime _secondDate;
        private int _firstSetHours;
        private int _firstSetMinutes;
        private int _secondSetHours;
        private int _secondSetMinutes;

        private bool _isMessages;
        private bool _isEvent;
        private bool _isError;
        private bool _isLightTheme = true;

        #endregion //Fields

        #region Properties

        public DateTime FirstDate
        {
            get => _firstDate;
            set => SetProperty(ref _firstDate, value);
        }

        public DateTime SecondDate
        {
            get => _secondDate;
            set => SetProperty(ref _secondDate, value);
        }

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

        public bool IsEvent
        {
            get => _isEvent;
            set
            {
                if (_isError)
                {
                    SetProperty(ref _isEvent, false);
                    return;
                }
                else
                {
                    SetProperty(ref _isEvent, value);
                }
            }
        }

        public bool IsError
        {
            get => _isError;
            set
            {
                if (_isEvent)
                {
                    SetProperty(ref _isError, false);
                    return;
                }
                else
                {
                    SetProperty(ref _isError, value);
                }
            }
        }

        public DelegateCommand FindCommand { get; set; }

        public DelegateCommand CloseCommand { get; set; }

        #endregion //Properties

        public EventLogViewModel(IEventAggregator eventAggregator, IEventLogHandler eventLogHandler)
        {
            _eventAggregator = eventAggregator;
            _eventLogHandler = eventLogHandler;

            _eventAggregator.GetEvent<OpenEventLogEvent>().Subscribe(OnOpenEventLog);
            _eventAggregator.GetEvent<CloseWindowEvent>().Subscribe(OnCloseEventLog);
            _eventAggregator.GetEvent<ChangeThemeEvent>().Subscribe(OnChangeThemeEventLog);

            _firstDate = DateTime.Today;
            _secondDate = DateTime.Now;

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
            FirstDate = new DateTime(FirstDate.Year, FirstDate.Month, FirstDate.Day);
            SecondDate = new DateTime(SecondDate.Year, SecondDate.Month, SecondDate.Day);

            if (FirstDate <= SecondDate)
            {
                var selectedEvents = IsEvent ? EventType.Event : EventType.Error;

                _eventLogHandler.SendFiltrationRequest(FirstDate, SecondDate, selectedEvents);
                _eventAggregator.GetEvent<OpenChatEvent>().Publish();
                ViewVisibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Incorrect date range!");
            }
        }

        private void ExecuteCloseCommand()
        {
            _eventAggregator.GetEvent<OpenChatEvent>().Publish();
            ViewVisibility = Visibility.Collapsed;
        }

        private void OnOpenEventLog()
        {
            ViewVisibility = Visibility.Visible;
        }

        private void OnCloseEventLog()
        {
            IsEvent = false;
            IsError = false;
            ViewVisibility = Visibility.Collapsed;
        }

        private void OnChangeThemeEventLog(bool isLightTheme)
        {
            IsLightTheme = isLightTheme;
        }
    }
}
