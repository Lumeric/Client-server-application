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
        private ObservableCollection<TabItem> _privateGroups = new ObservableCollection<TabItem>();
        private TabItem _group;

        public string PrivateGroupName { get; set; }

        public ObservableCollection<TabItem> PrivateGroups
        {
            get => _privateGroups;
            set => SetProperty(ref _privateGroups, value);
        }

        private bool _isSelected = false;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                ShowSelectedGroup();
            }
        }

        public TabItem Group 
        { 
            get => _group; 
            set => SetProperty(ref _group, value);
        }

        public DelegateCommand AddTab { get; set; }

        public GroupListViewModel()
        {
            _group = new TabItem();
            _group.Header = "General";
            _group.Content = "GeneralGroupMessages";
            _privateGroups.Add(_group);

            AddTab = new DelegateCommand(ExecuteAddTab).ObservesProperty(() => PrivateGroups);
        }


        private void ShowSelectedGroup()
        {
            if (IsSelected)
            {
                _group.Content = "SelectedGroupMessages";
            }
        }

        public void ExecuteAddTab()
        {
            _group = new TabItem();
            string s = "1";
            string p = "2";
            _group.Header = s.ToString();
            _group.Content = p.ToString();
            _privateGroups.Add(_group);         
        }
    }
}
