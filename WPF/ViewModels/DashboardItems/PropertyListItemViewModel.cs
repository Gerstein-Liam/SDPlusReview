using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands.Generics;
using WPF.CustomArcGisLibrary.Lib;

namespace WPF.ViewModels.DashboardItems
{
    public class PropertyListItemViewModel : ObservableObject
    {









        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private double _Area;
        public double Area
        {
            get
            {
                return _Area;
            }
            set
            {
                _Area = value;
                OnPropertyChanged(nameof(Area));
            }
        }

        private double _centerX;
        public double CenterX
        {
            get
            {
                return _centerX;
            }
            set
            {
                _centerX = value;
                OnPropertyChanged(nameof(CenterX));
            }
        }
        private double _centerY;
        public double CenterY
        {
            get
            {
                return _centerY;
            }
            set
            {
                _centerY = value;
                OnPropertyChanged(nameof(CenterY));
            }
        }
        #region DebugData
        private int _verticesCount;
        public int VerticesCount
        {
            get
            {
                return _verticesCount;
            }
            set
            {
                _verticesCount = value;
                OnPropertyChanged(nameof(VerticesCount));
            }
        }

        private double _viewScale;
        public double ViewScale
        {
            get
            {
                return _viewScale;
            }
            set
            {
                _viewScale = value;
                OnPropertyChanged(nameof(ViewScale));
            }
        }



        public string ID { get; }

        public string OwnerID { get; } 
        #endregion


        public PropertyListItemViewModel(string id, string ownerid, string name, OwnerListItemViewModel proprietary,
                                        double CenterX,
                                        double CenterY,
                                        int VerticesCount,
                                        double viewScale, double Area

                                        )
        {

            ID = id;
            OwnerID = ownerid;
            Name = name;

            //_proprietary = proprietary;
            this.CenterX = CenterX;
            this.CenterY = CenterY;
            this.VerticesCount = VerticesCount;
            ViewScale = viewScale;
            this.Area = Area;



        }
    }
}
