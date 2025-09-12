using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF.Commands.Generics;
using WPF.Models;
using WPF.Services.HttpServices.Backend;
using WPF.Services.HttpServices.GeoAdmin;
using WPF.ViewModels;
using WPF.ViewModels.DashboardItems;

namespace WPF.Commands.Specifics
{
    

    public class SearchAdressesDbCommand: AsyncCommandBase
    {

        private SearchAdressViewModel _viewModel;
        private GeoAdminFindAdresseHTTPClient _httpWebApiService;
        private Action _onQueryCompleted;
        public string AdressToSearch;



        public SearchAdressesDbCommand(SearchAdressViewModel viewModel, GeoAdminFindAdresseHTTPClient httpWebApiService)
        {
            _viewModel = viewModel;
            _httpWebApiService = httpWebApiService;
            _onQueryCompleted = _viewModel.OnAdressQueryCompleted;

        }



        public override async Task TaskToExecute()
        {
            try
            {


                _viewModel.AdressResult = await _httpWebApiService.Get_Async<GeoAdminAdressResultDTO>(AdressToSearch);
                _onQueryCompleted?.Invoke();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }
    }
}
