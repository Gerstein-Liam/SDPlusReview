using Dumpify;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using WPF.Commands.Specifics;
using WPF.DevUtils;
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

        private AdressItemViewModel _selectedAdress;
        public AdressItemViewModel SelectedAdress
        {
            get
            {
                return _selectedAdress;
            }
            set
            {
                _selectedAdress = value;
                OnPropertyChanged(nameof(SelectedAdress));
                _onAdressSelection?.Invoke(_selectedAdress.Adress,_selectedAdress.ZIP,_selectedAdress.Coordinates);
            }
        }

        public SearchAdressesDbCommand SearchAdressDBCommandBt { get; }
        private readonly GeoAdminFindAdresseHTTPClient _geoAdminHttpClient;

        public GeoAdminAdressResultDTO AdressResult;

        public ObservableCollection<AdressItemViewModel> SearchResults { get; set; }

        private Action<string,string,Map_Point> _onAdressSelection;

        private readonly ILogger<SearchAdressViewModel> _logger;

        public SearchAdressViewModel(GeoAdminFindAdresseHTTPClient geoAdminHttpClient, ILogger<SearchAdressViewModel> logger)
        {
				_geoAdminHttpClient = geoAdminHttpClient;
       
            SearchAdressDBCommandBt = new SearchAdressesDbCommand(this, _geoAdminHttpClient);
            SearchResults = new ObservableCollection<AdressItemViewModel>();
            _logger = logger;   
        }

        public void Subcribe(Action<string,string,Map_Point> onAdressSelection) {

            _onAdressSelection = onAdressSelection;

        }


        public void OnAdressQueryCompleted()
        {
            Debug.WriteLine(this.AdressResult);
            _logger.LogInformation("ThisAdressResult");
            this.AdressResult.DumpMe();
            SearchResults.Clear();
            //SelectedAdress = null;
            if (AdressResult != null)
            {
                foreach (var address in AdressResult.results)
                {

                    string Adress = "";
                    string Zip = "";

                   



                    var AdressPattern = @"(?<=)(.*)(?=\<b)";
                    Match AdressMatch = Regex.Match(address.attrs.label, AdressPattern);

                    var ZipPattern = @"(?<=\>)(.*)(?=\<)";
                    Match ZipMatch = Regex.Match(address.attrs.label, ZipPattern);

                    if (AdressMatch.Success)
                    {

                        //Console.WriteLine($"Zip {AdressMatch.Value}");
                        Adress = AdressMatch.Value;
                    }

                    if (ZipMatch.Success)
                    {

                        //Console.WriteLine($"Zip {AdressMatch.Value}");
                        Zip = ZipMatch.Value;
                    }


                    SearchResults.Add(new AdressItemViewModel(Adress,Zip, new Map_Point(address.attrs.x, address.attrs.y, 0)));
                   ;

                }
            }
        }

    }
}
