using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class Property
    {
        public string ID { get; set; }
        public string OwnerID { get; set; }

        public string PropertyName { get; set; }
        public string OwnerName { get; set; }

        public Map_Point? Center { get; set; }

        public double Area { get; set; }

        public double SnapScale { get; set; }
        public List<Map_Point>? Vertices { get; set; } = new List<Map_Point>();
        public int VerticeCount
        {
            get
            {

                return (Vertices?.Count ?? 0);
            }
        }

        public void SetCenter(double x, double y)
        {
            Center = new Map_Point(x, y);
        }


        public void PrintMe()
        {


            foreach (var item in Vertices)
            {
                Debug.WriteLine($"new Coordinates( {item.X},{item.Y},0),");
            }

        }

    }
}
