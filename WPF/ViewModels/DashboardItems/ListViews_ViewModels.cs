using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Perception.Spatial.Preview;
using WPF.Commands.Generics;
using WPF.CustomArcGisLibrary.Lib;
using WPF.Models;
namespace WPF.ViewModels.DashboardItems
{
    public class ListViews_ViewModels : ObservableObject
    {


        public ICommand ToggleDebugMode { get; }


        private bool _showDebugData=false;
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




        public ObservableCollection<OwnerListItemViewModel> ProprietaryList { get; set; }
        private OwnerListItemViewModel _selectedProprietary;
        public OwnerListItemViewModel SelectedProp
        {
            get
            {
                return _selectedProprietary;
            }
            set
            {
                _selectedProprietary = value;
                OnPropertyChanged(nameof(SelectedProp));
                //if (value != null) onProprietarySelection(_selectedProprietary);
                onProprietarySelection(_selectedProprietary);
            }
        }


        private bool IgnoreViewCentering=false;

        private PropertyListItemViewModel _selectedExploitation;
        public PropertyListItemViewModel SelectedExploitation
        {
            get
            {
                return _selectedExploitation;
            }
            set
            {
                _selectedExploitation = value;
                OnPropertyChanged(nameof(SelectedExploitation));
                if(!IgnoreViewCentering) _onExploitationSelection?.Invoke(_selectedExploitation);
                IgnoreViewCentering = false;
            }
        }
        private readonly Action< PropertyListItemViewModel> _onExploitationSelection;
        private Action<OwnerListItemViewModel> onProprietarySelection;
        public ListViews_ViewModels(Action<OwnerListItemViewModel> onOwnerSelection, Action<PropertyListItemViewModel> onPropertySelection)
        {
            ProprietaryList = new();
            onProprietarySelection = onOwnerSelection;
            _onExploitationSelection = onPropertySelection;
            ToggleDebugMode = new ActionCommand(() => {
                
                ShowDebugData = !ShowDebugData; 
            
            
            });

        }


        public void SelectProperty(string ownerID, string propertyID) {

            OwnerListItemViewModel? Target = ProprietaryList.Where(x => x.ID == ownerID).FirstOrDefault();
            if (Target != null) { 
            
                   SelectedProp=Target;
                PropertyListItemViewModel? property = Target.ExploitationList.Where(x => x.ID == propertyID).FirstOrDefault();
                if (property != null) {
                    IgnoreViewCentering=true;
                    SelectedExploitation = property;
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
            ProprietaryList.Add(newProprierty);
        }
        public void AddExploitation(OwnerListItemViewModel proprietayViewModel, Property exploitationItem)
        {
            proprietayViewModel.ExploitationList.Add(new PropertyListItemViewModel(exploitationItem.ID, exploitationItem.OwnerID, exploitationItem.PropertyName,
                                                    proprietayViewModel,
                                                    exploitationItem.Center.X,
                                                    exploitationItem.Center.Y,
                                                    exploitationItem.Vertices.Count(), exploitationItem.SnapScale, exploitationItem.Area));
        }
        public void AddExploitationUsingID(string ProprietryID, Property exploitationItem)
        {
            exploitationItem.PrintMe();
            OwnerListItemViewModel? Target = ProprietaryList.Where(x => x.ID == ProprietryID).FirstOrDefault();
            if (Target != null)
            {
                AddExploitation(Target, exploitationItem);
            }
        }
        public void Reset()
        {
            this.ProprietaryList.Clear();
            this.SelectedProp = null;
        }
        public void RemoveExploitation(OwnerListItemViewModel ownerItem, PropertyListItemViewModel propertyItem)
        {
            ownerItem.ExploitationList.Remove(propertyItem);
        }
        public void RemoveExploitationCloneAsEntry(OwnerListItemViewModel ownerItem, Property propertyItem)
        {
            PropertyListItemViewModel? originalPropertyItem = ownerItem.ExploitationList.Where(x => x.ID == propertyItem.ID).FirstOrDefault();
            if (originalPropertyItem != null) RemoveExploitation(ownerItem, originalPropertyItem);
        }
        public void UpdateExploitationUsingID(string ProprietryID, Property exploitationItem)
        {
            OwnerListItemViewModel? Target = ProprietaryList.Where(x => x.ID == ProprietryID).FirstOrDefault();
            if (Target != null)
            {
                PropertyListItemViewModel t = Target.ExploitationList.Where(x => x.ID == exploitationItem.ID).First();
                t.Area = exploitationItem.Area;
                t.VerticesCount = exploitationItem.Vertices.Count();
            }
        }
        public void RemoveExploitationUsingID(string ownerID, Property propertyItem)
        {
            //  HierarchicalPrint.Hierarchical_Print(HierarchicalPrint.ListView, "ListVM", "RemoveExploitationUsingID", $"eventData:{ownerID}:{propertyItem.ID}");
            OwnerListItemViewModel? Target = ProprietaryList.Where(x => x.ID == ownerID).FirstOrDefault();
            if (Target != null)
            {
                RemoveExploitationCloneAsEntry(Target, propertyItem);
            }
        }
    }
}