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

                if (_viewModel.ApplicationData.Owners != null && _viewModel.ApplicationData.Owners.Count > 0)
                {
                    await _httpWebApiService.PostAllOwner(_viewModel.ApplicationData.Owners);

                }
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
