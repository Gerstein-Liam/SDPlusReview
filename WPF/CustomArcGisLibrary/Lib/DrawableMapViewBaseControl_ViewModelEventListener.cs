using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Models;

namespace WPF.CustomArcGisLibrary.Lib
{
    public partial class DrawableMapViewBaseControl
    {


 
        
        private void OnViewChangeQuery(Viewpoint viewpoint)
        {
            this.SetViewpoint(viewpoint);
        }


        private void OnTargetCollectionEvent(GraphicsOverlay graphicsOverlay)
        {
            this.TargetCollection = graphicsOverlay;
        }

        private void OnEditionsRulesChange(MapInteractionContext Rules)
        {

            mapInteractionContext.AllowAdding = Rules.AllowAdding;
            this._interactionRulesChanged?.Invoke(mapInteractionContext);

        }

    }
}
