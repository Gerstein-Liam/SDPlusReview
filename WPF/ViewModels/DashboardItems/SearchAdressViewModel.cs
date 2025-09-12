using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Commands.Specifics;
using WPF.Models;
using WPF.Services.HttpServices.GeoAdmin;

namespace WPF.ViewModels.DashboardItems
{
    public class SearchAdressViewModel : ViewModelBase 
    {
		private string _adressToSearch;
		public string AdressToSearch
		{
			get
			{
				return _adressToSearch;
			}
			set
			{
				_adressToSearch = value;
				OnPropertyChanged(nameof(AdressToSearch));
                SearchAdressDBCommandBt.AdressToSearch = value;

            }
		}
        public SearchAdressesDbCommand SearchAdressDBCommandBt { get; }
        private readonly GeoAdminFindAdresseHTTPClient _geoAdminHttpClient;

        public GeoAdminAdressResultDTO AdressResult;

        public ObservableCollection<AdressItemViewModel> SearchResults { get; set; }
        public SearchAdressViewModel(GeoAdminFindAdresseHTTPClient geoAdminHttpClient)
        {
				_geoAdminHttpClient = geoAdminHttpClient;
            SearchAdressDBCommandBt = new SearchAdressesDbCommand(this, _geoAdminHttpClient);
            SearchResults = new ObservableCollection<AdressItemViewModel>();    
        }

        public void OnAdressQueryCompleted()
        {
            Debug.WriteLine(this.AdressResult);

            SearchResults.Clear();
            //SelectedAdress = null;
            if (AdressResult != null)
            {
                foreach (var address in AdressResult.results)
                {
                    SearchResults.Add(new AdressItemViewModel(address.attrs.detail, new Map_Point(address.attrs.x, address.attrs.y, 0)));
                }
            }
        }

    }
}
