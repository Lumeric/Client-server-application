using Client.BusinessLogic;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private BindableBase _selectedViewModel = new LoginViewModel();

        public BindableBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { _selectedViewModel = value; } 
        }

        public ICommand UpdateView { get; set; }

        public MainWindowViewModel()
        {
            UpdateView = new UpdateView(this);
        }

    }
}
