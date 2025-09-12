using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ViewModels
{
    public class HomeViewModel:ViewModelBase
    {


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



        public HomeViewModel()
        {
            Test = "Home ViewModel est binded!";
        }
    }
}
