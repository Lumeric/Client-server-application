using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class GroupListViewModel : BindableBase
    {
        public string PrivateGroupName { get; set; }

        public List<string> PrivateChats { get; set; }

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

        public GroupListViewModel()
        {
            //this code breaks the stack
            //PrivateChats = new List<string>();
            //PrivateChats.Add("1111");
            //PrivateChats.Add("1111");
            //PrivateChats.Add("1111");
        }

        private void ShowSelectedGroup()
        {
            if (IsSelected)
                Console.WriteLine("Showed new group");
        }
    }
}
