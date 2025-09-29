using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands.Generics;

namespace WPF.ViewModels.DashboardItems
{
    public class CurrentSelectionInfoViewModel: ViewModelBase 
    {

		private string _ownerName;
		public string OwnerName
		{
			get
			{
				return _ownerName;
			}
			set
			{
				_ownerName = value;
				OnPropertyChanged(nameof(OwnerName));
			}
		}


		private string _propertyName;
		public string PropertyName
		{
			get
			{
				return _propertyName;
			}
			set
			{
				_propertyName = value;
				OnPropertyChanged(nameof(PropertyName));
			}
		}


		private string _adress;
		public string Adress
		{
			get
			{
				return _adress;
			}
			set
			{
				_adress = value;
				OnPropertyChanged(nameof(Adress));
			}
		}



		private string _zip;
		public string ZIP
		{
			get
			{
				return _zip;
			}
			set
			{
				_zip = value;
				OnPropertyChanged(nameof(ZIP));
			}
		}

		private double _propertySurface;
		public double PropertySurface
		{
			get
			{
				return _propertySurface;
			}
			set
			{
				_propertySurface = value;
				OnPropertyChanged(nameof(PropertySurface));
			}
		}



        public double[] Values1 { get; set; } =
      [50, 60, 60, 70, 75, 80, 80, 70, 60, 60, 60, 60];



        public string[] Labels { get; set; } =
            ["Jan", "Fev", "Mars", "Avr", "Mai", "Juin", "Juil", "Aout", "Sept", "Oct", "Nov", "Dec"];

        public Func<double, string> Labeler { get; set; } = value => value + "%";

		public ICommand ChangeValuesBt { get; }


        public CurrentSelectionInfoViewModel()
        {
			ChangeValuesBt = new ActionCommand(() => {

                Values1 = [50, 60, 60, 100, 100, 80, 80, 70, 60, 60, 60, 60];

                OnPropertyChanged(nameof(Values1));
            });
        }


        public void SetField(string ownerName,string propertyName, string Adress,string ZIP, double propertySurface, double[] Temp)
		{
			OwnerName = ownerName;
			PropertyName = propertyName;
			PropertySurface = propertySurface;
			this.ZIP= ZIP;
			this.Adress = Adress;	
            Values1= Temp;
			OnPropertyChanged(nameof(Values1));	

        }

		public void ClearField() {
		
		
		
		}
	}
}
