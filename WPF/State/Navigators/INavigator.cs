using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.ViewModels;

namespace WPF.State.Navigators
{
    public enum ViewType
    {
        Home,
        Login,
        Dashboard,       // Le dashboard est instancier une seul fois
        Settings
    }

    public interface INavigator
    {

        bool ShowDashboard { get; set; }
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;

    }
}
