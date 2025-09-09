using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands.Specifics;
using WPF.Delegates;
using WPF.State.Navigators;
using WPF.ViewModels.Factories;
namespace WPF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly INavigator _navigator;
        private string _test;
        public string Test
        {
            get
            {
                return _test;
            }
            set
            {
                _test = value;
                OnPropertyChanged(nameof(Test));
            }
        }
        public DashboardViewModel DashboardViewModel { get; }
        private bool _showDashboard;
        public bool ShowDashboard
        {
            get
            {
                return _showDashboard;
            }
            set
            {
                _showDashboard = value;
                OnPropertyChanged(nameof(ShowDashboard));
            }
        }
        //public bool ShowDashboard=>_navigator.ShowDashboard;
        public bool IsLoggedIn => true;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
        public ICommand UpdateCurrentViewModelCommand { get; }
        public ICommand ShowDashboardCommand { get; }
        public MainWindowViewModel(INavigator navigator, IViewModelFactory viewModelFactory, CreateViewModel<DashboardViewModel> createDashboardViewModel)
        {
            _navigator = navigator;
            _navigator.StateChanged += Navigator_StateChanged;
            DashboardViewModel = createDashboardViewModel();
            _viewModelFactory = viewModelFactory;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory, OnViewTypeChange);
            UpdateCurrentViewModelCommand.Execute(ViewType.Dashboard);
            Test = "OK";
        }
        private void OnViewTypeChange(bool showDashboard)
        {
            ShowDashboard = showDashboard;
        }
        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}