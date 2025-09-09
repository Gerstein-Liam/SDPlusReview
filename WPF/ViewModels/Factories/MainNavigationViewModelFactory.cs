using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Delegates;
using WPF.State.Navigators;

namespace WPF.ViewModels.Factories
{


 

    public class MainNavigationViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<DashboardViewModel> _createDashboardViewModel;
        private readonly CreateViewModel<SettingsViewModel> _createSettingsViewModel;




        public MainNavigationViewModelFactory(CreateViewModel<DashboardViewModel> createDashboardViewModel, CreateViewModel<HomeViewModel> createHomeViewModel, CreateViewModel<SettingsViewModel> createSettingsViewModel)
        {
            _createDashboardViewModel = createDashboardViewModel;
            _createHomeViewModel = createHomeViewModel;
            _createSettingsViewModel = createSettingsViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _createHomeViewModel();
                    break;
                case ViewType.Dashboard:
                    return _createDashboardViewModel();
                    break;
                case ViewType.Settings:
                    return _createSettingsViewModel();
                    break;
                default:
                    throw new ArgumentException("The View Type does not have a ViewmModel", "viewType");

            }
        }
    }
}
