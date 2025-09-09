using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using WPF.Commands.Generics;
using WPF.Commands.Specifics;
using WPF.CustomArcGisLibrary.ExampleUsage;
using WPF.CustomArcGisLibrary.Lib;
using WPF.Models;
using WPF.Services.HttpServices;
using WPF.State.Context;
using WPF.ViewModels.DashboardItems;
namespace WPF.ViewModels
{
    public partial class DashboardViewModel : ViewModelBase
    {
        public ICommand GetAllFromApiBt { get; }
        public ICommand SetAllToApiBt { get; }

        public FakeDb FakeDatabaseJson = new();
        public AppData ApplicationData = new AppData();
        public List<Owner> ownerList;
        public ListViews_ViewModels ListViewModel { get; }
        public AbstractMapViewModel<List<Property>, Property> MapViewModel { get; }
        private readonly IBackendHTTPService _backendHTTPService;
        public DashboardViewModel(AbstractMapViewModel<List<Property>, Property> MapViewModel, IApplicationContext<Settings> SettingsContext, IBackendHTTPService backendHTTPService)
        {
            ClearAppDataBt = new ActionCommand(ClearAppData);
            DumpAppDataBt = new ActionCommand(DumpAppData);
            DumpMapCollectionlBt = new ActionCommand(DumpMapCollection);
            DumpListViewModelBt = new ActionCommand(DumpListViewCollection);
            this.MapViewModel = MapViewModel;
            this.MapViewModel.SubscribeToViewEvent(OnGraphicCollectionChanged);
            _backendHTTPService = backendHTTPService;
            SettingsContext.onEventChangingApplicationContext += OnSettingsChange;
            ListViewModel = new(onProprietarySelectionFromList);
          //  GetAllFromApiBt = new GetAllFromApiCommand(this, _backendHTTPService);
          //  GetAllFromApiBt.Execute(null);
            SetAllToApiBt = new SetAllToApiCommand(this, _backendHTTPService);
            InitCollections();


        }
        private void InitCollections()
        {
       
            ListViewModel.ProprietaryList.Clear();
         
            FakeDatabaseJson.Proprietaires.ForEach(pDto =>
            {
                ApplicationData.AddProprietry(pDto);
                
            });
            //ICI ON ATTENDS QUE EVENT UPDATE
            foreach (var p in ApplicationData.Owners)
            {
                MapViewModel.CreateGraphicOverlay(p.ID, p.Properties.ToList());
            }
            foreach (var item in ApplicationData.Owners)
            {
                ListViewModel.AddProprietry(item, onExploitationSelectionFromList);
            }
        }
    }
}