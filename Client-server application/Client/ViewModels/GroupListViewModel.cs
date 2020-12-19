using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client.ViewModels
{
    public class GroupListViewModel : BindableBase
    {
        private List<TabItem> _privateChats = new List<TabItem>();
        private TabItem header;

        public string PrivateGroupName { get; set; }
        private ObservableCollection<TabItem> _obCollection = new ObservableCollection<TabItem>();

        public List<TabItem> PrivateChats 
        {
            get => _privateChats;
            set
            {
                _privateChats = value;
                RaisePropertyChanged(PrivateGroupName);
            }
        }

        public ObservableCollection<TabItem> ObCollection
        {
            get => _obCollection;
            set => SetProperty(ref _obCollection, value);
        }

        //private bool _isSelected;

        //public bool IsSelected
        //{
        //    get { return _isSelected; }
        //    set
        //    {
        //        _isSelected = value;
        //        ShowSelectedGroup();
        //        RaisePropertyChanged(nameof(IsSelected));
        //    }
        //}

        public DelegateCommand AddTab { get; set; }

        public GroupListViewModel()
        {
            //_privateChatName = new Dictionary<string, string>();
            //_privateChatName.Add("Header", "Message");
            //_privateChatName.Add("Header2", "Message2");

            

            AddTab = new DelegateCommand(ExecuteAddTab).ObservesProperty(() => ObCollection);
        }


        //private void ShowSelectedGroup()
        //{
        //    if (IsSelected)
        //        Console.WriteLine("Shown new group");
        //}

        public void ExecuteAddTab()
        {
            header = new TabItem();
            header.Content = "123";
            header.Header = "321";
            ObCollection.Add(header);         
        }
    }
}
