using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace WPF.ViewModels
{
    public partial class DashboardViewModel
    {
        private bool _showDebugPanel;
        public bool ShowDebugPanel { get => _showDebugPanel; }
        private bool _showBackenPanel = true;
        public bool ShowBackendPanel { get => _showBackenPanel; }
        public ICommand DumpListViewModelBt { get; }
        public ICommand ClearAppDataBt { get; }
        public ICommand DumpAppDataBt { get; }
        public ICommand DumpMapCollectionlBt { get; }
        private void ClearAppData()
        {
            if (ownerList != null) ownerList.Clear();
        }
        private void DumpAppData()
        {
            Debug.WriteLine(ownerList);
        }
        private void DumpMapCollection()
        {
            Debug.WriteLine(MapViewModel.MapOverlayCollection);
        }
        private void DumpListViewCollection()
        {
            Debug.WriteLine(MapViewModel.MapOverlayCollection);
        }
    }
}