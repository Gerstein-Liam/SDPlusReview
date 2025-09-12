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
using WPF.Services.HttpServices.Backend;
using WPF.Services.HttpServices.GeoAdmin;
using WPF.State.Context;
using WPF.ViewModels.DashboardItems;
namespace WPF.ViewModels
{
    public partial class DashboardViewModel : ViewModelBase
    {

        public AsyncCommandBase SyncDBCommandBt { get; }

  


        public FakeDb FakeDatabaseJson = new();
        public AppData ApplicationData = new AppData();
        public List<Owner> ownerList;
        public ListViews_ViewModels ListViewModel { get; }
        public AbstractMapViewModel<List<Property>, Property> MapViewModel { get; }

        public SearchAdressViewModel SearchAdressViewModel { get; }

        private readonly IBackendHTTPService _backendHTTPService;
        private readonly GeoAdminFindAdresseHTTPClient _geoAdminHttpClient;
        public DashboardViewModel(AbstractMapViewModel<List<Property>, Property> MapViewModel, IApplicationContext<Settings> SettingsContext, IBackendHTTPService backendHTTPService, GeoAdminFindAdresseHTTPClient geoAdminHttpClient)
        {
            ClearAppDataBt = new ActionCommand(ClearAppData);
            DumpAppDataBt = new ActionCommand(DumpAppData);
            DumpMapCollectionlBt = new ActionCommand(DumpMapCollection);
            DumpListViewModelBt = new ActionCommand(DumpListViewCollection);
            this.MapViewModel = MapViewModel;
            this.MapViewModel.SubscriptOnCollectionChangedEvent(OnGraphicCollectionChanged);
            this.MapViewModel.SubscriptOnSelectionEvent(onGraphicSelectionEvent);
            _backendHTTPService = backendHTTPService;
            _geoAdminHttpClient = geoAdminHttpClient;
            SettingsContext.onEventChangingApplicationContext += OnSettingsChange;
            ListViewModel = new(onProprietarySelectionFromList, onExploitationSelectionFromList);
            ListViewModel.ShowDebugData=true;
            SyncDBCommandBt = new SyncDbCommand(this, _backendHTTPService);
            SyncDBCommandBt.AllowExecution(true);
            SyncDBCommandBt.Execute(null);
            SearchAdressViewModel= new SearchAdressViewModel(_geoAdminHttpClient); 
          
        }
        private void SetViewModelCollections()
        {
            ApplicationData.Owners.Clear();
            ApplicationData.Owners = ownerList;
            ListViewModel.ProprietaryList.Clear();
            MapViewModel?.MapOverlayCollection.Clear();
      
            foreach (var p in ApplicationData.Owners)
            {
                MapViewModel.CreateGraphicOverlay(p.ID, p.Properties.ToList());
            }
            foreach (var item in ApplicationData.Owners)
            {
                ListViewModel.AddProprietry(item);
            }
            this.MapViewModel.SetAddPermissions(false);
        }
    }
}