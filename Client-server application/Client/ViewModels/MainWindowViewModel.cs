using Client.BusinessLogic;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private Visibility _loginVisibility;
        private Visibility _chatVisibility;

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

        public MainWindowViewModel()
        {
            //_loginVisibility = Visibility.Visible;
            //_chatVisibility = Visibility.Collapsed;
            LoginVisibility = Visibility.Collapsed;
            ChatVisibility = Visibility.Visible;
        }


    }
}
