using DOMAIN;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Commands.Generics;
using WPF.Models;

namespace WPF.ViewModels.DashboardItems
{


    public class OwnerComboBoxItem
    {

        public string OwnerName { get; set; }

        public string OwnerId { get; set; }
    }
    
    public class PropertyEditViewModel: ViewModelBase 
    {


        private string _action;
        public string Action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
                OnPropertyChanged(nameof(Action));
            }
        }


        private bool _showMe;
        public bool ShowMe
        {
            get
            {
                return _showMe;
            }
            set
            {
                _showMe = value;
                OnPropertyChanged(nameof(ShowMe));
            }
        }


        private string _propertyName;
        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
            set
            {
                _propertyName = value;
                OnPropertyChanged(nameof(PropertyName));
            }
        }


        private string _currentOwnerName;
        public string CurrentOwnerName
        {
            get
            {
                return _currentOwnerName;
            }
            set
            {
                _currentOwnerName = value;
                OnPropertyChanged(nameof(CurrentOwnerName));
            }
        }


        private string _adress;
        public string Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
                OnPropertyChanged(nameof(Adress));
            }
        }

        private string _zip;
        public string Zip
        {
            get
            {
                return _zip;
            }
            set
            {
                _zip = value;
                OnPropertyChanged(nameof(Zip));
            }
        }


        private int _JanTemp = 50;
        public int JanTemp
        {
            get
            {
                return _JanTemp;
            }
            set
            {
                _JanTemp = value;
                OnPropertyChanged(nameof(JanTemp));
          
            }
        }

        private int _febTemp = 50;
        public int FebTemp
        {
            get
            {
                return _febTemp;
            }
            set
            {
                _febTemp = value;
                OnPropertyChanged(nameof(FebTemp));
             
            }
        }

        private int _marsTemp = 50;
        public int MarsTemp
        {
            get
            {
                return _marsTemp;
            }
            set
            {
                _marsTemp = value;
                OnPropertyChanged(nameof(MarsTemp));
       
            }
        }

        private int _avrTemp = 50;
        public int AvrTemp
        {
            get
            {
                return _avrTemp;
            }
            set
            {
                _avrTemp = value;
                OnPropertyChanged(nameof(AvrTemp));
             
            }
        }
        private int _maiTemp = 50;
        public int MaiTemp
        {
            get
            {
                return _maiTemp;
            }
            set
            {
                _maiTemp = value;
                OnPropertyChanged(nameof(MaiTemp));
          
            }
        }

        private int _junTemp = 50;
        public int JunTemp
        {
            get
            {
                return _junTemp;
            }
            set
            {
                _junTemp = value;
                OnPropertyChanged(nameof(JunTemp));
            
            }
        }


        private int _julTemp = 50;
        public int JulTemp
        {
            get
            {
                return _julTemp;
            }
            set
            {
                _julTemp = value;
                OnPropertyChanged(nameof(JulTemp));
             
            }
        }
        private int _augTemp = 50;
        public int AugTemp
        {
            get
            {
                return _augTemp;
            }
            set
            {
                _augTemp = value;
                OnPropertyChanged(nameof(AugTemp));
             
            }
        }

        private int _sepTemp = 50;
        public int SepTemp
        {
            get
            {
                return _sepTemp;
            }
            set
            {
                _sepTemp = value;
                OnPropertyChanged(nameof(SepTemp));
               
            }
        }



        private int _octTemp = 50;
        public int OctTemp
        {
            get
            {
                return _octTemp;
            }
            set
            {
                _octTemp = value;
                OnPropertyChanged(nameof(OctTemp));
               
            }
        }
        private int _novTemp = 50;
        public int NovTemp
        {
            get
            {
                return _novTemp;
            }
            set
            {
                _novTemp = value;
                OnPropertyChanged(nameof(NovTemp));
             
            }
        }

        private int _decTemp = 50;
        public int DecTemp
        {
            get
            {
                return _decTemp;
            }
            set
            {
                _decTemp = value;
                OnPropertyChanged(nameof(DecTemp));
              
            }
        }

        private bool _showTransferPanale;
        public bool ShowTransferePanel
        {
            get
            {
                return _showTransferPanale;
            }
            set
            {
                _showTransferPanale = value;
                OnPropertyChanged(nameof(ShowTransferePanel));
            }
        }


        public ObservableCollection<OwnerComboBoxItem> TargetOwner { get; } =new ObservableCollection<OwnerComboBoxItem>();

        private OwnerComboBoxItem _selectedTargetOwner;
        public OwnerComboBoxItem SelectedTargetOwner
        {
            get
            {
                return _selectedTargetOwner;
            }
            set
            {
                _selectedTargetOwner = value;
                OnPropertyChanged(nameof(SelectedTargetOwner));
            }
        }

        public ActionCommand ValidedBt { get; }

        private Action<Property,string?> _editedAddedProperty;
        public PropertyEditViewModel(Action<Property, string?> editedAddedProperty)
        {
                _editedAddedProperty = editedAddedProperty;


            ValidedBt = new ActionCommand(() =>
            {

                SunRate sunRate = new SunRate();
                sunRate.January = JanTemp;
                sunRate.February = FebTemp;
                sunRate.Mars = MarsTemp;
                sunRate.April = AvrTemp;
                sunRate.May = MaiTemp;
                sunRate.Jun = JunTemp;
                sunRate.July = JulTemp;
                sunRate.August = AugTemp;
                sunRate.September = SepTemp;
                sunRate.October = OctTemp;
                sunRate.November = NovTemp;
                sunRate.December = DecTemp;

                Property property = new Property() { OwnerName = "-", ID = "-", PropertyName= PropertyName,Adress=Adress,ZIP=Zip, SunRate=sunRate};

                if (SelectedTargetOwner != null)
                {

                    _editedAddedProperty(property, SelectedTargetOwner.OwnerId);
                }
                else
                {
                    _editedAddedProperty(property, null);
                }


            
            });


  
        }

       
    }
}
