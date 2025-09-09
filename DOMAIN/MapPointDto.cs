using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN
{
    public class MapPointDto
    {

        public string ID { get; set; } = Guid.NewGuid().ToString();

        public double X { get; set; }

        public double Y { get; set; }


        public int GeoIndex { get; set; }
        public MapPointDto(double x, double y, int geoIndex)
        {
            X = x; Y = y; GeoIndex = geoIndex;
        }

        public MapPointDto()
        {

        }
    }
}
