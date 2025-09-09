using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.CustomArcGisLibrary.Lib
{
    public interface IArcGisAbstractViewModelViewContext
    {

        public void GetEditionsContext();

        public Map MapView { get; set; }

        public ArcGisCustomEvents ArcGisCustomEvents { get; set; }

        public GraphicsOverlayCollection MapOverlayCollection { get; set; }

        public GraphicsOverlay TargetCollection { get; set; }

    }
}
