using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.CustomArcGisLibrary.Lib
{
    public class ArcGisCustomEvents
    {

        // Trigger: View      / Observer: ViewVodel
        public EventHandler<NotifyCollectionChangedEventArgs> ViewEvent;     // --> Les collections ArcGis ne genere pas Event pour l'update,donc faut la generer soi meme depuis la vue                     
        //Trigger: ViewModel / Observer: View
        public Action<Viewpoint> ViewPointChanged;
        public Action<GraphicsOverlay> TargetCollectionChanged;
        public Action<MapInteractionContext> InteractionSetRulesChangedFromViewModel;

    }
}
