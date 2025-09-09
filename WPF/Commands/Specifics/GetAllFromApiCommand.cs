using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF.Services.HttpServices;
using WPF.ViewModels;
namespace WPF.Commands.Specifics
{
    public class GetAllFromApiCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private DashboardViewModel _viewModel;
        private IBackendHTTPService _httpWebApiService;
        private Action _onQueryCompleted;
        public GetAllFromApiCommand(DashboardViewModel viewModel, IBackendHTTPService httpWebApiService)
        {
            _viewModel = viewModel;
            _httpWebApiService = httpWebApiService;
            _onQueryCompleted = _viewModel.OnQueryCompleted;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }
        public async void Execute(object? parameter)
        {
            try
            {
                _viewModel.ownerList = await _httpWebApiService.GetAllOwner();
                _onQueryCompleted?.Invoke();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}