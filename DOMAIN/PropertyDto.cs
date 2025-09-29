using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN
{
    
    
    public class SunRate
    {

        public int January { get; set; } 

        public int February{ get; set; } 

        public int Mars { get; set; } 

        public int April { get; set; }

        public int May { get; set; } 

        public int Jun { get; set; } 

        public int July{ get; set; } 

        public int August { get; set; }

        public int September{ get; set; } 

        public int October { get; set; } 

        public int November { get; set; }

        public int December { get; set; } 

    

    }



    public class PropertyDto
    {

        public string ID { get; set; } = Guid.NewGuid().ToString();

        public string OwnerID { get; set; } = "-";
        public string PropertyName { get; set; }= "-";
        public string OwnerName { get; set; } = "-";

        public string Adress { get; set; } = "?";

        public string ZIP { get; set; } = "?";

        public List<MapPointDto>? Vertices { get; set; } = new List<MapPointDto>();

        public SunRate SunRate { get; set; }  = new SunRate();

        public PropertyDto()
        {

        }
        public PropertyDto(string propertyName, string ownerName, List<MapPointDto>? vertices)
        {
            PropertyName = propertyName;
            OwnerName = ownerName;
            Vertices = vertices;
        }
    }
}
