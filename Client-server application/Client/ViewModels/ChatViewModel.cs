using Client.BusinessLogic;
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
    public class ChatViewModel : BindableBase
    {
        private Visibility _viewVisibility;

        public Visibility ViewVisibility
        {
            get => _viewVisibility;
            set => SetProperty(ref _viewVisibility, value);
        }

        public ChatViewModel(IEventAggregator eventAggregator)
        {
            _viewVisibility = Visibility.Collapsed;
            eventAggregator.GetEvent<UserValidatedEvent>().Subscribe(ChangeVisibility);
        }

        private void ChangeVisibility(Visibility obj)
        {
            ViewVisibility = obj;
        }
    }
}
