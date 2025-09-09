using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class Map_Point
    {


        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public int GeoIndex { get; set; }
        public Map_Point(double x = 0, double y = 0, double z = 0)
        {
            X = x; Y = y; Z = z;
        }
    }
}
