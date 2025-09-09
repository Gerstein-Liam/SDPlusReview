using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.State.Navigators;
using WPF.ViewModels.Factories;

namespace WPF.Commands.Specifics
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly Action<bool> _onViewTypeChange;
        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory, Action<bool> OnViewTypeChange)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _onViewTypeChange = OnViewTypeChange;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;



                if (viewType == ViewType.Dashboard)
                {
                    _onViewTypeChange(true);
                    // _navigator.ShowDashboard = true; 


                }
                else
                {
                    _onViewTypeChange(false);
                    //  _navigator.ShowDashboard = false;
                    _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
                }




            }
        }
    }
}
