using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF.CustomArcGisLibrary.ExampleUsage;
using WPF.Models;
using WPF.State.Context;
using WPF.ViewModels.DashboardItems;

namespace WPF.ViewModels
{
    public partial class DashboardViewModel
    {

        public void OnQueryCompleted()
        {
            SetViewModelCollections();
        }
    


        //From Map

        void OnGraphicCollectionChanged(ConcreteMapViewModel.EventData<Property> eventData)
        {

            if (eventData.EventType == NotifyCollectionChangedAction.Add.ToString())
            {
                ApplicationData.AddExploitationUsingID(eventData.GroupID, eventData.Item);
                ListViewModel.AddExploitationUsingID(eventData.GroupID, eventData.Item);
            }
            if (eventData.EventType == NotifyCollectionChangedAction.Remove.ToString())
            {
                ApplicationData.RemoveExploitationUsingID(eventData.GroupID, eventData.Item);
                ListViewModel.RemoveExploitationUsingID(eventData.GroupID, eventData.Item);
            }
            if (eventData.EventType == NotifyCollectionChangedAction.Replace.ToString())
            {
                ApplicationData.UpdateExploitationUsingID(eventData.GroupID, eventData.Item);
                ListViewModel.UpdateExploitationUsingID(eventData.GroupID, eventData.Item);
            }

        }




        //From ListViewModel

        private void onExploitationSelectionFromList(PropertyListItemViewModel exploitation)
        {
            if (exploitation != null) MapViewModel.CameraCenterOnCustomCoordinatesSystem(exploitation.CenterX, exploitation.CenterY, exploitation.ViewScale);
        }
        private void onProprietarySelectionFromList(OwnerListItemViewModel prop)
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

        private void onGraphicSelectionEvent(string ownerId,string propertyId)
        {


            Debug.WriteLine($"ownerID:{ownerId}  propertyId:{propertyId}");
            ListViewModel.SelectProperty(ownerId, propertyId);
           

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
