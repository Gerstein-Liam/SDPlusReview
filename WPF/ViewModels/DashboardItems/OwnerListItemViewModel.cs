using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.CustomArcGisLibrary.Lib;

namespace WPF.ViewModels.DashboardItems
{
    public class OwnerListItemViewModel : ObservableObject
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private int _exploitationsNumber;
        public int ExploitationNumbers
        {
            get
            {
                return _exploitationsNumber;
            }
            set
            {
                _exploitationsNumber = value;
                OnPropertyChanged(nameof(ExploitationNumbers));
            }
        }


   


        public string ID { get; }
     
        public ObservableCollection<PropertyListItemViewModel> Properties { get; set; }
        public OwnerListItemViewModel(string id, string name, int exploitationNumber)
        {

            ID = id;
            Properties = new ObservableCollection<PropertyListItemViewModel>();
            this.Name = name;
           
        }
        public void AddExploitation(PropertyListItemViewModel newExploitation)
        {
            // newExploitation._proprietary = this;
            Properties.Add(newExploitation);
            this.ExploitationNumbers = Properties.Count;
        }
    }
}
