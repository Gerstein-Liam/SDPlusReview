using Esri.ArcGISRuntime.Mapping;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Storage;
using WPF.CustomArcGisLibrary.ExampleUsage;
using WPF.CustomArcGisLibrary.Lib;
using WPF.Models;
using WPF.State.Context;
using WPF.ViewModels.DashboardItems;
using WPF.Views.DashboardItems;
namespace WPF.ViewModels
{
    public partial class DashboardViewModel
    {

        public void onAddOwnerRequest()
        {
            OwnerEditVM.ShowMe = true;
            PropertyEditVM.ShowMe = false;

            userRequestActionContext.Action = UserRequestAction.AddOwner.ToString();


        }


        public void onUpdateOwnerRequest(string ownerId)
        {
            OwnerEditVM.ShowMe = true;
            PropertyEditVM.ShowMe = false;
            userRequestActionContext.Action = UserRequestAction.EditOwner.ToString();
            userRequestActionContext.OwnerID = ownerId;
        }

        public void onDeleteOwnerRequest(string ownerId)
        {

            userRequestActionContext.Action = UserRequestAction.RemoveOwner.ToString();
            userRequestActionContext.OwnerID = ownerId;
            OnOwnerEditDialogClosed(null);
        }


        public void onAddPropertyRequest(string ownerID)
        {
            OwnerEditVM.ShowMe = false;
            PropertyEditVM.ShowMe = true;
            PropertyEditVM.ShowTransferePanel = false;
            PropertyEditVM.Action = "Add Property";
            userRequestActionContext.Action = UserRequestAction.AddProperty.ToString();
        }

        public void onEditPropertyRequest(string ownerId, string propertyID)
        {
            OwnerEditVM.ShowMe = false;
            PropertyEditVM.ShowTransferePanel = true;

            PropertyEditVM.ShowMe = true;
            PropertyEditVM.Action = "Edit Property";
            Property p=ApplicationData.GetProperty(ownerId, propertyID);
            PropertyEditVM.PropertyName = p.PropertyName;
            PropertyEditVM.Adress=p.Adress;
            PropertyEditVM.Zip = p.ZIP;
            PropertyEditVM.JanTemp = p.SunRate.January;
            PropertyEditVM.FebTemp = p.SunRate.February;
            PropertyEditVM.MarsTemp = p.SunRate.Mars;
            PropertyEditVM.AvrTemp = p.SunRate.April;
            PropertyEditVM.MaiTemp = p.SunRate.May;
            PropertyEditVM.JunTemp = p.SunRate.Jun;
            PropertyEditVM.JulTemp = p.SunRate.July;
            PropertyEditVM.AugTemp = p.SunRate.August;
            PropertyEditVM.SepTemp = p.SunRate.September;
            PropertyEditVM.OctTemp = p.SunRate.October;
            PropertyEditVM.NovTemp = p.SunRate.November;
            PropertyEditVM.DecTemp = p.SunRate.December;
            userRequestActionContext.Action = UserRequestAction.EditProperty.ToString();
            userRequestActionContext.OwnerID= p.OwnerID;
            userRequestActionContext.PropertyID = p.ID;


            PropertyEditVM.TargetOwner.Clear();
            foreach (var owner in ApplicationData.OwnersList)
            {
                PropertyEditVM.TargetOwner.Add(new OwnerComboBoxItem() { OwnerName = owner.OwnerName, OwnerId = owner.ID });
            }
            PropertyEditVM.CurrentOwnerName = p.OwnerName;  
          
        }

        public void onDeletePropertyRequest(string ownerId, string propertyID)
        {
            Property p = ApplicationData.GetProperty(ownerId, propertyID);
            ListViewModel.RemoveExploitationUsingID(p.OwnerID, p);
            ApplicationData.RemovePropertyUsingOwnerID(p.OwnerID, p);

            MapViewModel.RemoveGraphic(p.OwnerID, p.ID);
        }



        private void OnDrawRequest(Property property,string TargetOwnerID=null)
        {


            userRequestActionContext.currentPropertyEdited = property;

            PropertyEditVM.ShowMe = false;
            if (property != null ) 
            { 
                
                if(userRequestActionContext.Action == UserRequestAction.AddProperty.ToString()) this.MapViewModel.StartDraw(property.PropertyName);

                if (userRequestActionContext.Action == UserRequestAction.EditProperty.ToString()) {
                    Property p = ApplicationData.GetProperty(userRequestActionContext.OwnerID, userRequestActionContext.PropertyID);
                    p.PropertyName=PropertyEditVM.PropertyName ;
                    p.Adress=PropertyEditVM.Adress ;
                    p.ZIP = PropertyEditVM.Zip;
                    p.SunRate.January= PropertyEditVM.JanTemp;
                    p.SunRate.February = PropertyEditVM.FebTemp;
                    p.SunRate.Mars = PropertyEditVM.MarsTemp;
                    p.SunRate.April = PropertyEditVM.AvrTemp;
                    p.SunRate.May = PropertyEditVM.MaiTemp;
                    p.SunRate.Jun = PropertyEditVM.JunTemp;
                    p.SunRate.July = PropertyEditVM.JulTemp;
                    p.SunRate.August = PropertyEditVM.AugTemp;
                    p.SunRate.September = PropertyEditVM.SepTemp;
                    p.SunRate.October = PropertyEditVM.OctTemp;
                    p.SunRate.November = PropertyEditVM.NovTemp;
                    p.SunRate.December = PropertyEditVM.DecTemp;
                    userRequestActionContext.Action = "";
                    userRequestActionContext.OwnerID = "";
                    userRequestActionContext.PropertyID = "";
                    ListViewModel.UpdateExploitationUsingID(p.OwnerID, p);
                    PropertyEditVM.SelectedTargetOwner = null;
                    if (TargetOwnerID != null) {

                        ApplicationData.TransferProperty(p.OwnerID, TargetOwnerID, p.ID);
                        SetViewModelCollections();
                    }

                    CurrentSelectionVM.SetField(p.OwnerName, p.PropertyName, p.Adress, p.ZIP, p.Area,

             new double[] {
                    p. SunRate.January,
                    p. SunRate.February,
                    p. SunRate.Mars,
                    p. SunRate.April,
                    p. SunRate.May,
                    p. SunRate.Jun,
                    p. SunRate.July,
                    p. SunRate.September,
                    p. SunRate.October,
                    p. SunRate.November,
                    p. SunRate.December,




         });

                    // SetViewModelCollections();
                }

            }
           
        }


        private void OnOwnerEditDialogClosed(Owner owner)
        {

            OwnerEditVM.ShowMe = false;
            if (userRequestActionContext.Action == UserRequestAction.AddOwner.ToString())
            {

                ApplicationData.OwnersList.Add(owner);

            }
            if (userRequestActionContext.Action == UserRequestAction.EditOwner.ToString())
            {

                ApplicationData.updateOwner(userRequestActionContext.OwnerID, owner);

            }

            if (userRequestActionContext.Action == UserRequestAction.RemoveOwner.ToString())
            {

                ApplicationData.DeleteOwner(userRequestActionContext.OwnerID);

            }
            userRequestActionContext.Action = "";
            userRequestActionContext.OwnerID = "";
            userRequestActionContext.PropertyID = "";
            SetViewModelCollections();
        }





        public void OnQueryCompleted()
        {
            SetViewModelCollections();
            ListViewModel.RestoreList();
        }
        private void OnAdressSelection(string adress, string ZIP, Map_Point coordinates)
        {
            this.MapViewModel.CameraCenterOnCustomCoordinatesSystem(coordinates.X, coordinates.Y, 500);
            this.PropertyEditVM.Adress = adress;
            this.PropertyEditVM.Zip = ZIP;
        }
        //From Map





        void IAbstactMapVMListener<Property>.onGraphicSelectionEvent(string ownerId, string propertyId)
        {
            Debug.WriteLine($"ownerID:{ownerId}  propertyId:{propertyId}");
            ListViewModel.SelectProperty(ownerId, propertyId);


            Property p = ApplicationData.GetProperty(ListViewModel.SelectedOwner.ID, ListViewModel.SelectedProperty.ID);
            CurrentSelectionVM.SetField(p.OwnerName, p.PropertyName,p.Adress,p.ZIP, p.Area,

                new double[] {
                    p. SunRate.January,
                    p. SunRate.February,
                    p. SunRate.Mars,
                    p. SunRate.April,
                    p. SunRate.May,
                    p. SunRate.Jun,
                    p. SunRate.July,
                    p. SunRate.September,
                    p. SunRate.October,
                    p. SunRate.November,
                    p. SunRate.December,




            });
        }


        void IAbstactMapVMListener<Property>.OnGraphicCollectionChanged(EventData<Property> eventData)
        {
            if (eventData.EventType == NotifyCollectionChangedAction.Add.ToString())
            {

                if (userRequestActionContext.Action == UserRequestAction.AddProperty.ToString())
                {
                    eventData.Item.SunRate = userRequestActionContext.currentPropertyEdited.SunRate;
                    eventData.Item.ZIP = userRequestActionContext.currentPropertyEdited.ZIP;
                    eventData.Item.Adress= userRequestActionContext.currentPropertyEdited.Adress;
                    userRequestActionContext.Action = "";
                }

                ApplicationData.AddPropertyUsingOwnerID(eventData.GroupID, eventData.Item);
                ListViewModel.AddExploitationUsingID(eventData.GroupID, eventData.Item);
            }
            if (eventData.EventType == NotifyCollectionChangedAction.Remove.ToString())
            {
                ApplicationData.RemovePropertyUsingOwnerID(eventData.GroupID, eventData.Item);
                ListViewModel.RemoveExploitationUsingID(eventData.GroupID, eventData.Item);
            }
            if (eventData.EventType == NotifyCollectionChangedAction.Replace.ToString())
            {
                ApplicationData.UpdatePropertyUsingOwnerID(eventData.GroupID, eventData.Item);
                ListViewModel.UpdateExploitationUsingID(eventData.GroupID, eventData.Item);
            }
        }
        //From ListViewModel
        void IListingViewModelListener.onPropertySelectionFromListingVM(PropertyListItemViewModel exploitation)
        {
            if (exploitation != null)
            {
                MapViewModel.CameraCenterOnCustomCoordinatesSystem(exploitation.CenterX, exploitation.CenterY, exploitation.ViewScale);

                Property p = ApplicationData.GetProperty(ListViewModel.SelectedOwner.ID, ListViewModel.SelectedProperty.ID);


                if (p != null)


                {

                    CurrentSelectionVM.SetField(p.OwnerName, p.PropertyName, p.Adress, p.ZIP, p.Area,

              new double[] {
                            p. SunRate.January,
                            p. SunRate.February,
                            p. SunRate.Mars,
                            p. SunRate.April,
                            p. SunRate.May,
                            p. SunRate.Jun,
                            p. SunRate.July,
                            p. SunRate.September,
                            p. SunRate.October,
                            p. SunRate.November,
                            p. SunRate.December});


                }
                else
                {
                    CurrentSelectionVM.ClearField();




                }
            }
            else
            {
                CurrentSelectionVM.ClearField();
            }
        }
        void IListingViewModelListener.onOwnerSelectionFromListingVM(OwnerListItemViewModel prop)
        {
            if (prop != null)
            {
                MapViewModel.SetAddPermissions(true);
                MapViewModel.SetTargetCollection(prop.ID);
            }
            else
            {
                MapViewModel.SetAddPermissions(false);
                MapViewModel.SetTargetCollection();
            }
        }



        //From IApplicationContext
        private void OnSettingsChange(Settings settings)
        {
            Debug.WriteLine($"1){settings.WorkWithoutDBConnection}");
            _showDebugPanel = settings.EnableDebugging;
            _showBackenPanel = !settings.WorkWithoutDBConnection;
            ListViewModel.ShowDebugData = _showDebugPanel;
            OnPropertyChanged(nameof(ShowBackendPanel));
            OnPropertyChanged(nameof(ShowDebugPanel));
        }
    }
}