using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF.Commands.Generics;
using WPF.Services.HttpServices.Backend;
using WPF.ViewModels;

namespace WPF.Commands.Specifics
{
 
    public class SyncDbCommand : AsyncCommandBase
    {
       
        private DashboardViewModel _viewModel;
        private IBackendHTTPService _httpWebApiService;
        private Action _onQueryCompleted;

      

        public SyncDbCommand(DashboardViewModel viewModel, IBackendHTTPService httpWebApiService)
        {
            _viewModel = viewModel;
            _httpWebApiService = httpWebApiService;
            _onQueryCompleted = _viewModel.OnQueryCompleted;
           
        }
    


        public override async Task TaskToExecute()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                });

                if (_viewModel.ApplicationData.OwnersList != null && _viewModel.ApplicationData.OwnersList.Count > 0)
                {
                    await _httpWebApiService.PostAllOwner(_viewModel.ApplicationData.OwnersList);

                }
               
                
                _viewModel.ListViewModel.SaveIndex();   
                _viewModel.ApplicationData.OwnersList = await _httpWebApiService.GetAllOwner();
                _onQueryCompleted?.Invoke();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Mouse.OverrideCursor = Cursors.Arrow;
                });

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Mouse.OverrideCursor = Cursors.Arrow;
                });

            }
        }
    }
}
