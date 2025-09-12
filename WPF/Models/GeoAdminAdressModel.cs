using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class Attrs
    {
        public string detail { get; set; }
        public string featureId { get; set; }
        public string geom_quadindex { get; set; }
        public string geom_st_box2d { get; set; }
        public string label { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public int num { get; set; }
        public string objectclass { get; set; }
        public string origin { get; set; }
        public int rank { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public int zoomlevel { get; set; }
    }

    public class Result
    {
        public Attrs attrs { get; set; }
        public int id { get; set; }
        public int weight { get; set; }
    }

    public class GeoAdminAdressResultDTO
    {
        public List<Result> results { get; set; }
    }
}
