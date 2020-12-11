using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.BusinessLogic
{
    public class UpdateView : ICommand
    {
        private MainWindowViewModel mwViewModel;

        public UpdateView(MainWindowViewModel vm)
        {
            this.mwViewModel = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true; 
        }

        public void Execute(object parameter)
        {
            if(parameter.ToString() == "Chat")
            {
                mwViewModel.SelectedViewModel = new LoginViewModel();
            }
        }
    }
}
