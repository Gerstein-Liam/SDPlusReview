using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using Windows.Perception.Spatial.Preview;
using WPF.Commands.Generics;
using WPF.CustomArcGisLibrary.Lib;
using WPF.Models;
namespace WPF.ViewModels.DashboardItems
{



    public interface IListingViewModelListener
    {
        void onPropertySelectionFromListingVM(PropertyListItemViewModel exploitation);
        void onOwnerSelectionFromListingVM(OwnerListItemViewModel prop);
        void onAddOwnerRequest();
        void onUpdateOwnerRequest(string ownerId);
        void onDeleteOwnerRequest(string ownerId);
        void onAddPropertyRequest(string ownerId);
        void onEditPropertyRequest(string ownerId,string propertyID);
        void onDeletePropertyRequest(string ownerId, string propertyID);


    }
    public class ListViews_ViewModels : ObservableObject
    {
        public ICommand AddOwner { get; }

        public ICommand UpdateOwner { get; }

        public ICommand DeleteOwner { get; }

        public ActionCommand AddProperty { get; }

        public ActionCommand EditProperty { get; }

        public ActionCommand DeleteProperty { get; }

        

        public ICommand ToggleDebugMode { get; }
        private bool _showDebugData = false;
        public bool ShowDebugData
        {
            get
            {
                return _showDebugData;
            }
            set
            {
                _showDebugData = value;
                OnPropertyChanged(nameof(ShowDebugData));
            }
        }
        public ObservableCollection<OwnerListItemViewModel> Owners { get; set; }



        private int _selectedOwnerIndex;
        public int SelectedOwnerIndex
        {
            get
            {
                return _selectedOwnerIndex;
            }
            set
            {
                _selectedOwnerIndex = value;
                OnPropertyChanged(nameof(SelectedOwnerIndex));
            }
        }

        public int _ownerIndex;
        public int _propertyIndex;


        private int _selectedPropertyIndex;
        public int SelectedPropertyIndex
        {
            get
            {
                return _selectedPropertyIndex;
            }
            set
            {
                _selectedPropertyIndex = value;
                OnPropertyChanged(nameof(SelectedPropertyIndex));
            }
        }


        private OwnerListItemViewModel selectedOwner;
        public OwnerListItemViewModel SelectedOwner
        {
            get
            {
                return selectedOwner;
            }
            set
            {
                selectedOwner = value;
                OnPropertyChanged(nameof(SelectedOwner));
                _onOwnerSelection(selectedOwner);
                AddProperty.RaiseCanExecuteChanged();
            }
        }
        private bool IgnoreViewCentering = false;
        private PropertyListItemViewModel selectedProperty;
        public PropertyListItemViewModel SelectedProperty
        {
            get
            {
                return selectedProperty;
            }
            set
            {
                selectedProperty = value;
                OnPropertyChanged(nameof(SelectedProperty));
                if (!IgnoreViewCentering) _onPropertySelection?.Invoke(selectedProperty);
                IgnoreViewCentering = false;
                EditProperty.RaiseCanExecuteChanged();
                DeleteProperty.RaiseCanExecuteChanged();

            }
        }
        private Action<PropertyListItemViewModel> _onPropertySelection;
        private Action<OwnerListItemViewModel> _onOwnerSelection;
        private Action _onAddOwnerRequest;
        private Action<string> _onUpdateRequest;
        private Action<string> _onDeleteRequest;
        private Action<string> _onAddPropertyRequest;
        private Action<string,string> _onEditPropertyRequest;
        private Action<string, string> _onDeletePropertyRequest;



        public ListViews_ViewModels()
        {
            Owners = new();
            ToggleDebugMode = new ActionCommand(() =>
            {
                ShowDebugData = !ShowDebugData;
            });
            AddOwner = new ActionCommand(() =>
            {
                _onAddOwnerRequest?.Invoke();
            });

            UpdateOwner= new ActionCommand(() =>
            {
                _onUpdateRequest?.Invoke(SelectedOwner.ID);
            });

            DeleteOwner = new ActionCommand(() =>
            {
                _onDeleteRequest?.Invoke(SelectedOwner.ID);
            });


            AddProperty= new ActionCommand(() =>
            {

                if (SelectedOwner != null) {

                    _onAddPropertyRequest?.Invoke(SelectedOwner.ID);
                }

            }, (o) => { 
                return SelectedOwner !=null; 
            
            
            });


            EditProperty = new ActionCommand(() =>
            {

                if (SelectedOwner != null)
                {

                    _onEditPropertyRequest?.Invoke(SelectedProperty.OwnerID, SelectedProperty.ID);
                }

            }, (o) => {
                return SelectedProperty != null;


            });


            DeleteProperty = new ActionCommand(() =>
            {

                if (SelectedOwner != null)
                {

                    _onDeletePropertyRequest?.Invoke(SelectedProperty.OwnerID, SelectedProperty.ID);
                }

            }, (o) => {
                return SelectedProperty != null;


            });





        }
        public void Subcribe(IListingViewModelListener subscriber)
        {
            _onOwnerSelection = subscriber.onOwnerSelectionFromListingVM;
            _onPropertySelection = subscriber.onPropertySelectionFromListingVM;
            _onAddOwnerRequest = subscriber.onAddOwnerRequest;
            _onUpdateRequest = subscriber.onUpdateOwnerRequest;
            _onDeleteRequest = subscriber.onDeleteOwnerRequest;
            _onAddPropertyRequest = subscriber.onAddPropertyRequest;
            _onEditPropertyRequest=subscriber.onEditPropertyRequest;
            _onDeletePropertyRequest = subscriber.onDeletePropertyRequest;


        }
        public void SelectProperty(string ownerID, string propertyID)
        {
            OwnerListItemViewModel? Target = Owners.Where(x => x.ID == ownerID).FirstOrDefault();
            if (Target != null)
            {
                SelectedOwner = Target;
                PropertyListItemViewModel? property = Target.Properties.Where(x => x.ID == propertyID).FirstOrDefault();
                if (property != null)
                {
                    IgnoreViewCentering = true;
                    SelectedProperty = property;
                }
            }
        }
        public void AddProprietry(Owner pItem)
        {
            OwnerListItemViewModel newProprierty = new OwnerListItemViewModel(pItem.ID, pItem.OwnerName, pItem.Properties.Count());
            foreach (var ex in pItem.Properties)
            {
                AddExploitation(newProprierty, ex);
            }
            Owners.Add(newProprierty);
        }
        public void AddExploitation(OwnerListItemViewModel proprietayViewModel, Property exploitationItem)
        {
            proprietayViewModel.Properties.Add(new PropertyListItemViewModel(exploitationItem.ID, exploitationItem.OwnerID, exploitationItem.PropertyName,
                                                    proprietayViewModel,
                                                    exploitationItem.Center.X,
                                                    exploitationItem.Center.Y,
                                                    exploitationItem.Vertices.Count(), exploitationItem.SnapScale, exploitationItem.Area, $"{exploitationItem.Adress} : {exploitationItem.ZIP}"));
        }
        public void AddExploitationUsingID(string ProprietryID, Property exploitationItem)
        {
            exploitationItem.PrintMe();
            OwnerListItemViewModel? Target = Owners.Where(x => x.ID == ProprietryID).FirstOrDefault();
            if (Target != null)
            {
                AddExploitation(Target, exploitationItem);
            }
        }
        public void Reset()
        {
            this.Owners.Clear();
            this.SelectedOwner = null;
        }
        public void RemoveExploitation(OwnerListItemViewModel ownerItem, PropertyListItemViewModel propertyItem)
        {
            ownerItem.Properties.Remove(propertyItem);
        }
        public void RemoveExploitationCloneAsEntry(OwnerListItemViewModel ownerItem, Property propertyItem)
        {
            PropertyListItemViewModel? originalPropertyItem = ownerItem.Properties.Where(x => x.ID == propertyItem.ID).FirstOrDefault();
            if (originalPropertyItem != null) RemoveExploitation(ownerItem, originalPropertyItem);
        }
        public void UpdateExploitationUsingID(string ProprietryID, Property exploitationItem)
        {
            OwnerListItemViewModel? Target = Owners.Where(x => x.ID == ProprietryID).FirstOrDefault();
            if (Target != null)
            {
                PropertyListItemViewModel t = Target.Properties.Where(x => x.ID == exploitationItem.ID).First();
                t.Name = exploitationItem.PropertyName;
                t.Area = exploitationItem.Area;
                t.VerticesCount = exploitationItem.Vertices.Count();
            }
        }
        public void RemoveExploitationUsingID(string ownerID, Property propertyItem)
        {
            //  HierarchicalPrint.Hierarchical_Print(HierarchicalPrint.ListView, "ListVM", "RemovePropertyUsingOwnerID", $"eventData:{ownerID}:{propertyItem.ID}");
            OwnerListItemViewModel? Target = Owners.Where(x => x.ID == ownerID).FirstOrDefault();
            if (Target != null)
            {
                RemoveExploitationCloneAsEntry(Target, propertyItem);
            }
        }

        public void SaveIndex() {

            _propertyIndex = SelectedPropertyIndex;
            _ownerIndex = SelectedOwnerIndex;   
        }

        public void RestoreList() {
           SelectedPropertyIndex= _propertyIndex;
           SelectedOwnerIndex= _ownerIndex;

        }
    }
}