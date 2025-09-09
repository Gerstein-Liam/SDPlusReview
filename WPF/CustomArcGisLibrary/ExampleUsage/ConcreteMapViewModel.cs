using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using WPF.CustomArcGisLibrary.Lib;
using WPF.Models;

namespace WPF.CustomArcGisLibrary.ExampleUsage
{
    public class ConcreteMapViewModel : AbstractMapViewModel<List<Property>, Property>
    {

        public ConcreteMapViewModel() : base()
        {
        }

        public ConcreteMapViewModel(Action<EventData<Property>> OnGraphicCollectedChangedLister) : base(OnGraphicCollectedChangedLister)
        {
        }
        public override Property ConvertBackGraphic(Graphic Item)
        {
            if (Item.Geometry != null)
            {
                List<Map_Point> Vertices = new();
                int VerticesIndex = 0;
                foreach (var p in ((Polygon)Item.Geometry).Parts[0].Points)
                {
                    Vertices.Add(new Map_Point(p.X, p.Y) { GeoIndex = VerticesIndex });
                    VerticesIndex++;
                }
                return new Property() { ID = Item.Attributes["ID"].ToString(), OwnerID = Item.Attributes["GID"].ToString(), Center = new Map_Point(Item.Geometry.Extent.GetCenter().X, Item.Geometry.Extent.GetCenter().Y), Vertices = Vertices, Area = Item.Geometry.Extent.Area() };
            }
            else
            {
                return new Property() { ID = Item.Attributes["ID"].ToString(), OwnerID = Item.Attributes["GID"].ToString() };

            }
        }

        //https://community.esri.com/t5/geoprocessing-questions/why-do-area-calculations-return-negative-values/td-p/596282
        public override Graphic ConvertGraphic(Property Item)
        {
            List<MapPoint> polygonPoints = new();
            foreach (var vertice in Item.Vertices)
            {
                polygonPoints.Add(new MapPoint(vertice.X, vertice.Y, SpatialReferences.WebMercator));
            }
            // Create polygon geometry.
            var Polygon = new Polygon(polygonPoints);
            // Create a fill symbol to display the polygon.
            var polygonSymbolOutline = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Blue, 2.0);
            var polygonFillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, System.Drawing.Color.Orange, polygonSymbolOutline);
            // Create a polygon graphic with the geometry and fill symbol.




            Graphic r = new Graphic(Polygon, polygonFillSymbol) { Attributes = { { "ID", Item.ID }, { "GID", Item.OwnerID } } };
            Item.SetCenter(r.Geometry.Extent.GetCenter().X, r.Geometry.Extent.GetCenter().Y);

            Item.Area = Math.Abs(r.Geometry.Area());
            Item.SnapScale = Item.Area * ScaleFactor;
            return r;
        }
        public override EventData<Property> CreateEventData(string EventType, string GroupID, string GraphicID, Graphic Item)
        {
            return new EventData<Property>(EventType, GroupID, GraphicID, ConvertBackGraphic(Item));
        }
    }
}
