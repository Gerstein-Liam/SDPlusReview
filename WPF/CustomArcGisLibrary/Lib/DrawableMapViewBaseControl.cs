using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using Esri.ArcGISRuntime.UI.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = System.Drawing.Color;
using System.Diagnostics;
namespace WPF.CustomArcGisLibrary.Lib
{
    public partial class DrawableMapViewBaseControl : MapView
    {

        private ArcGisCustomEvents _eventSystem;
        public GraphicsOverlay TargetCollection { get; set; }
        private Graphic? _selectedGraphic;
        private GeometryEditor _geometryEditor;

        private bool _allowVertexCreation = true;
        private bool AllowModification = false;


        private SimpleFillSymbol _polygonSymbol;

        private MapInteractionContext mapInteractionContext;

        private Action<MapInteractionContext> _interactionRulesChanged;

        public void SetContext(IArcGisAbstractViewModelViewContext Context, Action<MapInteractionContext> InteractionRulesChanged)
        {


            base.Map = Context.MapView;
            base.GraphicsOverlays = Context.MapOverlayCollection;



            this.TargetCollection = Context.TargetCollection;
            this._eventSystem = Context.ArcGisCustomEvents;


            this._eventSystem.ViewPointChanged = OnViewChangeQuery;
            this._eventSystem.TargetCollectionChanged = OnTargetCollectionEvent;
            this._eventSystem.InteractionSetRulesChangedFromViewModel = OnEditionsRulesChange;


            this._interactionRulesChanged = InteractionRulesChanged;
        }

        public DrawableMapViewBaseControl()
        {
            this.GeoViewTapped += CustomMapView_GeoViewTapped;
            this._geometryEditor = this.GeometryEditor;
            var polygonLineSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Dash, Color.Black, 1);
            _polygonSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, Color.FromArgb(70, 255, 0, 0), polygonLineSymbol);

            mapInteractionContext = new MapInteractionContext();
            mapInteractionContext.AllowEditing = false;
            mapInteractionContext.AllowAdding = false;

        }









        private async void CustomMapView_GeoViewTapped(object? sender, GeoViewInputEventArgs e)
        {
            Debug.Write("");
            if (!_geometryEditor.IsStarted)
            {
                try
                {
                    IReadOnlyList<IdentifyGraphicsOverlayResult> results = await this.IdentifyGraphicsOverlaysAsync(e.Position, 5, false);
                    if (_selectedGraphic != null)
                    {
                        _selectedGraphic.IsSelected = false;
                    }
                    _selectedGraphic = results.FirstOrDefault()?.Graphics?.FirstOrDefault();
                    if (_selectedGraphic != null)
                    {

                        string? graphicID = _selectedGraphic.Attributes["ID"].ToString();
                        string? collectionID = _selectedGraphic.Attributes["GID"].ToString();
                        this._eventSystem.onSelection(collectionID, graphicID);
                    }
                    else
                    {
                        Debug.WriteLine($"SELECTED VOID");
                    }
                }
                catch (Exception ex)
                {
                   // MessageBox.Show("Error: " + ex.Message, ex.GetType().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (_selectedGraphic == null) return;
                _selectedGraphic.IsSelected = true;
                GeometryType geometryType = _selectedGraphic.Geometry.GeometryType;
                if (mapInteractionContext.AllowEditing)
                {
                    _geometryEditor.Start(_selectedGraphic.Geometry);
                    _selectedGraphic.IsVisible = false;
                }
            }
            else
            {
             
                if (_geometryEditor.SelectedElement == null)
                {
                    IdentifyGeometryEditorResult result = await this.IdentifyGeometryEditorAsync(e.Position, 10);
                    if (result != null && result.Elements.Count > 0)
                    {
                        GeometryEditorElement element = result.Elements.FirstOrDefault();
                        if (element is GeometryEditorVertex vertex)
                        {
                            // await this.SetViewpointAsync(new Viewpoint(new MapPoint(vertex.Point.X, vertex.Point.Y, vertex.Point.SpatialReference)), TimeSpan.FromSeconds(0.3));
                            _geometryEditor.SelectVertex(vertex.PartIndex, vertex.VertexIndex);
                        }
                        else if (element is GeometryEditorMidVertex midVertex && _allowVertexCreation)
                        {
                            //   await this.SetViewpointAsync(new Viewpoint(new MapPoint(midVertex.Point.X, midVertex.Point.Y, midVertex.Point.SpatialReference)), TimeSpan.FromSeconds(0.3));
                            _geometryEditor.SelectMidVertex(midVertex.PartIndex, midVertex.SegmentIndex);
                        }
                    }
                }
                return;
            }
        }




    }
}
