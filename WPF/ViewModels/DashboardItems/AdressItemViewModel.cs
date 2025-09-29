using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Models;

namespace WPF.ViewModels.DashboardItems
{
  

    public class AdressItemViewModel
    {


        public AdressItemViewModel()
        {
            Coordinates = new();
        }

        public AdressItemViewModel(string adress,string zip, Map_Point coordinates)
        {
            Adress = adress;
            ZIP = zip;
            Coordinates = coordinates;
        }

        public string Adress { get; }

        public string ZIP { get; }

        public Map_Point Coordinates { get; set; }

        public string CoordinatesAsString
        {
            get
            {

                return $"{Coordinates.X},{Coordinates.Y}";
            }
        }



    }
}
