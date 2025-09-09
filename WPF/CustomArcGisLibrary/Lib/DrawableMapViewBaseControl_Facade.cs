using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.UI;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.CustomArcGisLibrary.Lib
{
    public partial class DrawableMapViewBaseControl
    {

        public void DrawNewItem()
        {

            if (!_geometryEditor.IsStarted)
            {
                _geometryEditor.Start(GeometryType.Polygon);
            }
        }

        public void SaveDraw(string NewItemName)
        {



            Esri.ArcGISRuntime.Geometry.Geometry geometry = _geometryEditor.Stop();
            if (geometry != null)
            {
                if (_selectedGraphic != null)
                {
                    _selectedGraphic.Geometry = geometry;
                    NotifyCollectionChangedEventArgs arg = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, (object)_selectedGraphic, (object)_selectedGraphic, 0);
                    _eventSystem.ViewEvent?.Invoke(this, arg);
                    _selectedGraphic.IsSelected = false;
                    _selectedGraphic.IsVisible = true;
                    _selectedGraphic = null;
                }
                else
                {
                    Graphic newGraphic = new Graphic(geometry, _polygonSymbol) { Attributes = { { "ID", NewItemName }, { "GID", TargetCollection.Id } } };
                    TargetCollection.Graphics.Add(newGraphic);
                }
            }

        }

        public void SetEditMode(bool setEditMode)
        {

            mapInteractionContext.AllowEditing = setEditMode;

        }

        public void Delete()
        {
            DeleteSelectedElement();
        }

        private void DeleteSelectedElement()
        {
            if (_geometryEditor.IsStarted && _geometryEditor.SelectedElement != null && _geometryEditor.SelectedElement.CanDelete)
            {

                _geometryEditor.DeleteSelectedElement();
            }
            else
            {
                if (_selectedGraphic != null)
                {
                    _selectedGraphic.IsSelected = false;
                    _selectedGraphic.IsVisible = false;
                    Debug.WriteLine($"REMOVE:       graphic {_selectedGraphic.Attributes["ID"]} from {_selectedGraphic.GraphicsOverlay.Id}");
                    _selectedGraphic.GraphicsOverlay.Graphics.Remove(_selectedGraphic);
                    _selectedGraphic = null;
                }
            }
        }


        public void DebugContext()
        {

            Debug.WriteLine("");
        }
    }
}
