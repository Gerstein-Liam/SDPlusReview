using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.CustomArcGisLibrary.Lib;
using WPF.Models;

namespace WPF.ViewModels.DashboardItems
{
    public class ExploitationViewModel : ObservableObject
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

        //public PropriateryViewModel _proprietary;

        private double _centerX;
        public double CenterX
        {
            get
            {
                return _centerX;
            }
            set
            {
                _centerX = value;
                OnPropertyChanged(nameof(CenterX));
            }
        }
        private double _centerY;
        public double CenterY
        {
            get
            {
                return _centerY;
            }
            set
            {
                _centerY = value;
                OnPropertyChanged(nameof(CenterY));
            }
        }
        private int _verticesCount;
        public int VerticesCount
        {
            get
            {
                return _verticesCount;
            }
            set
            {
                _verticesCount = value;
                OnPropertyChanged(nameof(VerticesCount));
            }
        }

        private double _viewScale;
        public double ViewScale
        {
            get
            {
                return _viewScale;
            }
            set
            {
                _viewScale = value;
                OnPropertyChanged(nameof(ViewScale));
            }
        }

        private double _Area;
        public double Area
        {
            get
            {
                return _Area;
            }
            set
            {
                _Area = value;
                OnPropertyChanged(nameof(Area));
            }
        }



        public ExploitationViewModel(string name, PropriateryViewModel proprietary,
                                        double CenterX,
                                        double CenterY,
                                        int VerticesCount,
                                        double viewScale, double Area

                                        )
        {
            Name = name;
            //_proprietary = proprietary;
            this.CenterX = CenterX;
            this.CenterY = CenterY;
            this.VerticesCount = VerticesCount;
            ViewScale = viewScale;
            this.Area = Area;
        }
    }
    public class PropriateryViewModel : ObservableObject
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
        private ExploitationViewModel _selectedExploitation;
        public ExploitationViewModel SelectedExploitation
        {
            get
            {
                return _selectedExploitation;
            }
            set
            {
                _selectedExploitation = value;
                OnPropertyChanged(nameof(SelectedExploitation));
                onExploitationSelection?.Invoke(this, _selectedExploitation);
            }
        }
        private readonly Action<PropriateryViewModel, ExploitationViewModel> onExploitationSelection;
        public ObservableCollection<ExploitationViewModel> ExploitationList { get; set; }
        public PropriateryViewModel(string name, int exploitationNumber, Action<PropriateryViewModel, ExploitationViewModel> onExploitationSelection)
        {
            ExploitationList = new ObservableCollection<ExploitationViewModel>();
            this.Name = name;
            this.onExploitationSelection = onExploitationSelection;
        }
        public void AddExploitation(ExploitationViewModel newExploitation)
        {
            // newExploitation._proprietary = this;
            ExploitationList.Add(newExploitation);
            this.ExploitationNumbers = ExploitationList.Count;
        }
    }
    public class ListViews_ViewModels : ObservableObject
    {
        public ObservableCollection<PropriateryViewModel> ProprietaryList { get; set; }
        private PropriateryViewModel _selectedProprietary;
        public PropriateryViewModel SelectedProp
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
        private Action<PropriateryViewModel> onProprietarySelection;
        public ListViews_ViewModels(Action<PropriateryViewModel> OnProprietarySelection)
        {
            ProprietaryList = new();
            onProprietarySelection = OnProprietarySelection;
        }
        public void AddProprietry(Owner pItem, Action<PropriateryViewModel, ExploitationViewModel> onExploitationSelection)
        {
            PropriateryViewModel newProprierty = new PropriateryViewModel(pItem.ID, pItem.Properties.Count(), onExploitationSelection);
            foreach (var ex in pItem.Properties)
            {
                AddExploitation(newProprierty, ex);
            }
            ProprietaryList.Add(newProprierty);
        }
        public void AddExploitation(PropriateryViewModel proprietayViewModel, Property exploitationItem)
        {
            proprietayViewModel.ExploitationList.Add(new ExploitationViewModel(exploitationItem.ID,
                                                    proprietayViewModel,
                                                    exploitationItem.Center.X,
                                                    exploitationItem.Center.Y,
                                                    exploitationItem.Vertices.Count(), exploitationItem.SnapScale, exploitationItem.Area));
        }
        public void AddExploitationUsingID(string ProprietryID, Property exploitationItem)
        {

            exploitationItem.PrintMe();


            PropriateryViewModel Target = ProprietaryList.Where(x => x.Name == ProprietryID).First();
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


        public void RemoveExploitation(PropriateryViewModel proprietaryViewModel, ExploitationViewModel exploitationItem)
        {
            proprietaryViewModel.ExploitationList.Remove(exploitationItem);


        }

        public void RemoveExploitationCloneAsEntry(PropriateryViewModel proprietaryViewModel, Property exploitationItemClone)
        {
            ExploitationViewModel original = proprietaryViewModel.ExploitationList.Where(x => x.Name == exploitationItemClone.ID).First();
            if (RemoveExploitation != null) RemoveExploitation(proprietaryViewModel, original);

        }
        public void UpdateExploitationUsingID(string ProprietryID, Property exploitationItem)
        {
            PropriateryViewModel Target = ProprietaryList.Where(x => x.Name == ProprietryID).FirstOrDefault();


            if (Target != null)
            {
                ExploitationViewModel t = Target.ExploitationList.Where(x => x.Name == exploitationItem.ID).First();


                t.Area = exploitationItem.Area;
                t.VerticesCount = exploitationItem.Vertices.Count();
            }

        }


        public void RemoveExploitationUsingID(string ProprietryID, Property exploitationItem)
        {

          //  HierarchicalPrint.Hierarchical_Print(HierarchicalPrint.ListView, "ListVM", "RemoveExploitationUsingID", $"eventData:{ProprietryID}:{exploitationItem.ID}");

            PropriateryViewModel Target = ProprietaryList.Where(x => x.Name == ProprietryID).First();
            if (Target != null)
            {
                RemoveExploitationCloneAsEntry(Target, exploitationItem);
            }
        }




    }
}
