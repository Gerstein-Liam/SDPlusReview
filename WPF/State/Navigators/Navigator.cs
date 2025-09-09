using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.ViewModels;

namespace WPF.State.Navigators
{
    public class Navigator : INavigator
    {

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                StateChanged?.Invoke();
                ;
            }
        }

        private bool _showDashboard;
        public bool ShowDashboard { get => _showDashboard; set => _showDashboard = value; }

        public event Action StateChanged;
    }
}
