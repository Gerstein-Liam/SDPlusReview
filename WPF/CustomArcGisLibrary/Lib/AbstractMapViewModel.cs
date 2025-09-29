using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Portal;
using Esri.ArcGISRuntime.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Models;

namespace WPF.CustomArcGisLibrary.Lib
{




    public interface IAbstactMapVMListener<T> {

        void onGraphicSelectionEvent(string overlayGroupID, string graphicID);
        void OnGraphicCollectionChanged(EventData<T> eventData);
    }
    
    
    public class EventData<CustomGraphicItem>
    {
        public string EventType;
        public string GroupID;
        public string GraphicID;
        public CustomGraphicItem Item;
        public EventData(string eventType, string groupID, string graphicID, CustomGraphicItem Item)
        {
            EventType = eventType;
            GroupID = groupID;
            GraphicID = graphicID;
            this.Item = Item;
        }
    }

    public abstract class AbstractMapViewModel<CustomGraphicCollection, CustomGraphicItem> :
                                                                     ObservableObject, IArcGisAbstractViewModelViewContext
                                                                                 where CustomGraphicCollection : IEnumerable<CustomGraphicItem>
    {
   
        private Map _map;
        public Map Map
        {
            get => _map;
            set
            {
                _map = value; OnPropertyChanged(nameof(Map));
            }
        }
        private Viewpoint _centerScreen;
        public Viewpoint CenterScreen
        {
            get
            {
                return _centerScreen;
            }
            set
            {
                _centerScreen = value;
                OnPropertyChanged(nameof(CenterScreen));
            }
        }
        private bool _EnableDrawing;
        public bool EnableDrawing
        {
            get
            {
                return _EnableDrawing;
            }
            set
            {
                _EnableDrawing = value;
                OnPropertyChanged(nameof(EnableDrawing));
            }
        }
        public GraphicsOverlayCollection? MapOverlayCollection { get; set; }
        private GraphicsOverlay _targetCollection;
        public GraphicsOverlay TargetCollection
        {
            get
            {
                return _targetCollection;
            }
            set
            {
                _targetCollection = value;
                OnPropertyChanged(nameof(TargetCollection));
            }
        }
        protected double ScaleFactor = 0.3;
        public void CheckCollection()
        {
            Debug.WriteLine($"Overlay count {MapOverlayCollection.Count()}");
            foreach (var overlay in MapOverlayCollection)
            {
                Debug.WriteLine($"Overlay ID-> {(string.IsNullOrEmpty(overlay.Id) ? "VIDE" : overlay.Id)}");
                foreach (var graphic in overlay.Graphics)
                {
                    Debug.WriteLine($"       ID {graphic.Attributes["ID"]}:GID {graphic.Attributes["GID"]}");
                }
            }
            Debug.WriteLine(MapOverlayCollection);
        }
        public abstract Graphic ConvertGraphic(CustomGraphicItem Item);
        public abstract CustomGraphicItem ConvertBackGraphic(Graphic Item);
        public abstract EventData<CustomGraphicItem> CreateEventData(string EventType, string GroupID, string GraphicID, Graphic Item);

        private Action<EventData<CustomGraphicItem>> onGraphicCollectionChangedEvent;
        public event PropertyChangedEventHandler? PropertyChanged;
        private ArcGisCustomEvents _eventSystem { get; set; }
        public ArcGisCustomEvents ArcGisCustomEvents { get => _eventSystem; set => _eventSystem = value; }
        public Map MapView { get => _map; set => Map = value; }

        private  Action<string, string> _onSelection;


        protected AbstractMapViewModel()
        {
            MapOverlayCollection = new GraphicsOverlayCollection();

            _map = new Map(SpatialReferences.WebMercator)
            {
                Basemap = new Basemap(BasemapStyle.ArcGISStreets),
                InitialViewpoint = new Viewpoint(new MapPoint(812963.8159509106, 5817673.664758366, SpatialReferences.WebMercator), 5000)
            };
            _eventSystem = new ArcGisCustomEvents();
            _eventSystem.onCollectionsChanged += GraphicsCollectionChanged;
            ArcGisCustomEvents = _eventSystem;

        }



        protected AbstractMapViewModel(Action<EventData<CustomGraphicItem>> OnGraphicCollectedChangedLister,Action<string,string> onSelection)
        {
            MapOverlayCollection = new GraphicsOverlayCollection();
            onGraphicCollectionChangedEvent = OnGraphicCollectedChangedLister;
            _map = new Map(SpatialReferences.WebMercator)
            {
                Basemap = new Basemap(BasemapStyle.ArcGISStreets),
                InitialViewpoint = new Viewpoint(new MapPoint(812963.8159509106, 5817673.664758366, SpatialReferences.WebMercator), 5000)
            };
            _eventSystem = new ArcGisCustomEvents();
            _eventSystem.onCollectionsChanged += GraphicsCollectionChanged;
            ArcGisCustomEvents = _eventSystem;
  

        }

        public void StartDraw(string DrawName)
        {


            _eventSystem.StartDraw?.Invoke(DrawName);
        }
        public void Subscribe(IAbstactMapVMListener<CustomGraphicItem> sub) {

            onGraphicCollectionChangedEvent = sub.OnGraphicCollectionChanged;
            _eventSystem.onSelection=sub.onGraphicSelectionEvent;
            
        }

        public void SubscriptOnSelectionEvent(Action<string, string> onSelection)
        {

            _eventSystem.onSelection = onSelection;

        }

        //public void SubscriptOnCollectionChangedEvent(Action<EventData<CustomGraphicItem>> OnGraphicCollectedChangedLister)
        //{

        //    onGraphicCollectionChangedEvent = OnGraphicCollectedChangedLister;

        //}

        private void EventHandler_CustomEvent(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private (int index, Graphic graphic) FindGraphic(string OverlayGroupId, string ItemID)
        {
            GraphicsOverlay graphicoverlay = MapOverlayCollection.Where(x => x.Id.Contains(OverlayGroupId)).First();
            Graphic graphic = graphicoverlay.Graphics.Where(x => x.Attributes["ID"].ToString().Contains(ItemID))?.First();
            int index = graphicoverlay.Graphics.IndexOf(graphic);
            return (index, graphic);
        }
        public void SetEditPermissions(bool EditEnable)
        {
            _eventSystem.InteractionSetRulesChangedFromViewModel(new MapInteractionContext() { AllowEditing = EditEnable });
        }

        public void SetAddPermissions(bool AddEnable)
        {
            _eventSystem.InteractionSetRulesChangedFromViewModel(new MapInteractionContext() { AllowAdding = AddEnable });
        }
        #region Camera
        public void CameraCenterOnGraphic(string OverlayGroupId, string ItemID)
        {
            var (index, graphic) = FindGraphic(OverlayGroupId, ItemID);
            CenterOnGraphic(graphic);
        }
        private void CenterOnGraphic(Graphic Item)
        {
            CenterScreen = new Viewpoint(new MapPoint(Item.Geometry.Extent.GetCenter().X, Item.Geometry.Extent.GetCenter().Y, SpatialReferences.WebMercator), Item.Geometry.Extent.XMax * Item.Geometry.Extent.YMax * 100000);
        }
        public void CameraCenterOnCustomCoordinatesSystem(double x, double y, double scale = 500)
        {
            
            if(scale==0) scale = 500;
            CenterScreen = new Viewpoint(new MapPoint(x, y, SpatialReferences.WebMercator), scale);
            this._eventSystem.ViewPointChanged(CenterScreen);
        }
        public void CameraCenterOnMapPoint(MapPoint MapPoint)
        {
        }
        #endregion
        #region CRUD_USE_BY_VM
        public void SetTargetCollection(string OverlayGroupId = null)
        {
            if (OverlayGroupId != null)
            {
                GraphicsOverlay gOverlay = MapOverlayCollection.Where(x => x.Id.Contains(OverlayGroupId)).First();
                if (gOverlay != null) _eventSystem.TargetCollectionChanged(gOverlay);
            }
            else
            {
                TargetCollection = null;
                _eventSystem.TargetCollectionChanged(null);
            }
        }
        public void CreateGraphicOverlay(string OverlayGroupId, CustomGraphicCollection collection)
        {
            GraphicsOverlay OverlayGroup = new GraphicsOverlay() { Id = OverlayGroupId };
            collection.ToList().ForEach(x =>
            {
                Graphic graphic = ConvertGraphic(x);
                onGraphicCollectionChangedEvent?.Invoke(CreateEventData(NotifyCollectionChangedAction.Replace.ToString(), graphic.Attributes["GID"].ToString(), graphic.Attributes["ID"].ToString(), graphic));
                //     x = ConvertBackGraphic(graphic);
                OverlayGroup.Graphics.Add(graphic);
            });
            OverlayGroup.Graphics.CollectionChanged += GraphicsCollectionChanged;
            MapOverlayCollection.Add(OverlayGroup);
        }
        // Listening change occurs from code-heding
        public void ReplaceGraphic(string OverlayGroupId, string ItemID, CustomGraphicItem Item)
        {
            GraphicsOverlay graphicoverlay = MapOverlayCollection.Where(x => x.Id.Contains(OverlayGroupId)).First();
            var (index, graphic) = FindGraphic(OverlayGroupId, ItemID);
            if (graphic != null) graphicoverlay.Graphics[index] = ConvertGraphic(Item);
        }
        public void AddGraphic(string OverlayGroupId, CustomGraphicItem Item)
        {
            Graphic graphic = ConvertGraphic(Item);
            Item = ConvertBackGraphic(graphic);
            GraphicsOverlay graphicoverlay = MapOverlayCollection.Where(x => x.Id.Contains(OverlayGroupId)).First();
            if (graphicoverlay != null) graphicoverlay.Graphics.Add(graphic);
        }
        public void RemoveGraphic(string OverlayGroupId, string ItemID)
        {
            GraphicsOverlay graphicoverlay = MapOverlayCollection.Where(x => x.Id.Contains(OverlayGroupId)).First();
            var (index, graphic) = FindGraphic(OverlayGroupId, ItemID);
            if (graphic != null) graphicoverlay.Graphics.Remove(graphic);
        }
        public void RemoveOverlayGroup(string OverlayGroupId)
        {
            GraphicsOverlay group = MapOverlayCollection.Where(x => x.Id == OverlayGroupId).First();
            group.Graphics.CollectionChanged -= GraphicsCollectionChanged;
            MapOverlayCollection.Remove(group);
        }
        #endregion
        private void GraphicsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            string _overlayId_ = string.Empty;
            string _graphicId_ = string.Empty;
            string _actionType_ = string.Empty;
            Graphic _graphic_ = null;
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                _actionType_ = e.Action.ToString();
                _graphic_ = (Graphic)e.OldItems[0];


                _graphicId_ = _graphic_.Attributes["ID"].ToString();
                _overlayId_ = _graphic_.Attributes["GID"].ToString();
            }
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                _actionType_ = e.Action.ToString();
                _graphic_ = (Graphic)e.NewItems[0];

              
                _graphicId_ = _graphic_.Attributes["ID"].ToString();
                _overlayId_ = _graphic_.Attributes["GID"].ToString();
            }
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                _actionType_ = e.Action.ToString();
                _graphic_ = (Graphic)e.NewItems[0];
                _graphicId_ = _graphic_.Attributes["ID"].ToString();
                _overlayId_ = _graphic_.Attributes["GID"].ToString();
            }
            //HierarchicalPrint.Hierarchical_Print(HierarchicalPrint.CustomControlViewModel, "AbstractMap", "OnGraphicCollectionChanged", $"eventData:{_actionType_}:OverlayId{_overlayId_}:{_graphicId_}");
            if (string.IsNullOrEmpty(_actionType_) || string.IsNullOrEmpty(_graphicId_) || _graphic_ == null) return;
            onGraphicCollectionChangedEvent?.Invoke(CreateEventData(e.Action.ToString(), _overlayId_, _graphicId_, _graphic_));
        }
        public void SetViewPoint()
        {
            throw new NotImplementedException();
        }
        public void GetEditionsContext()
        {
            Debug.WriteLine(" ");
        }
        //Replace From ViewModel
    }
}
