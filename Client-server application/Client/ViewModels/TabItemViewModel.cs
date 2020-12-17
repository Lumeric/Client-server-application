using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class TabItemViewModel : BindableBase
    {
        public string PrivateGroupName { get; set; }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                ShowSelectedGroup();
                RaisePropertyChanged(nameof(IsSelected));
            }
        }

        private void ShowSelectedGroup()
        {
            if (IsSelected)
                Console.WriteLine("Showed new group");
        }
    }
}
