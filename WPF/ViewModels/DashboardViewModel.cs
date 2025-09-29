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

    public enum UserRequestAction
    {

        AddOwner,
        RemoveOwner,
        EditOwner,
        AddProperty,
        RemoveProperty,
        EditProperty,

    }

    public class UserRequestActionContext {

        public string Action;
        public string OwnerID;
        public string PropertyID;
        public Property currentPropertyEdited;

    }



    public partial class DashboardViewModel : ViewModelBase, IListingViewModelListener, IAbstactMapVMListener<Property>
    {

        public AsyncCommandBase SyncDBCommandBt { get; }


        public OwnerEditViewModel OwnerEditVM { get; }

        public PropertyEditViewModel PropertyEditVM { get; }



        public FakeDb FakeDatabaseJson = new();
        public AppData ApplicationData = new AppData();
        public List<Owner> ownerList;


        public AbstractMapViewModel<List<Property>, Property> MapViewModel { get; }
        public ListViews_ViewModels ListViewModel { get; }
        public CurrentSelectionInfoViewModel CurrentSelectionVM { get; }
        public SearchAdressViewModel SearchAdressViewModel { get; }

        private readonly IBackendHTTPService _backendHTTPService;

        private Property PropertyAddedEdited;
         

        private UserRequestActionContext userRequestActionContext=new();
        public DashboardViewModel(AbstractMapViewModel<List<Property>, Property> MapViewModel,
                                  IApplicationContext<Settings> SettingsContext,
                                  ListViews_ViewModels listingVM,
                                  SearchAdressViewModel searchViewModel,
                                  IBackendHTTPService backendHTTPService)
        {
            ClearAppDataBt = new ActionCommand(ClearAppData);
            DumpAppDataBt = new ActionCommand(DumpAppData);
            DumpMapCollectionlBt = new ActionCommand(DumpMapCollection);
            DumpListViewModelBt = new ActionCommand(DumpListViewCollection);

            SettingsContext.onEventChangingApplicationContext += OnSettingsChange;
            this.MapViewModel = MapViewModel;
            this.MapViewModel.Subscribe(this);
            ListViewModel = listingVM;
            ListViewModel.Subcribe(this);
            ListViewModel.ShowDebugData = true;

            _backendHTTPService = backendHTTPService;


            SyncDBCommandBt = new SyncDbCommand(this, _backendHTTPService);
            SyncDBCommandBt.AllowExecution(true);
            SyncDBCommandBt.Execute(null);
            SearchAdressViewModel = searchViewModel;
            SearchAdressViewModel.Subcribe(OnAdressSelection);

            CurrentSelectionVM = new CurrentSelectionInfoViewModel();
            CurrentSelectionVM.ClearField();

            OwnerEditVM = new OwnerEditViewModel(OnOwnerEditDialogClosed);
            PropertyEditVM = new(OnDrawRequest);
            
        }

     
       


        private void SetViewModelCollections()
        {
            //ApplicationData.OwnersList.Clear();
            //ApplicationData.OwnersList = ownerList;
            ListViewModel.Owners.Clear();
            MapViewModel?.MapOverlayCollection.Clear();

            foreach (var p in ApplicationData.OwnersList)
            {
                MapViewModel.CreateGraphicOverlay(p.ID, p.Properties.ToList());
            }
            foreach (var item in ApplicationData.OwnersList)
            {
                ListViewModel.AddProprietry(item);
            }
            this.MapViewModel.SetAddPermissions(false);
        }

        
    }
}