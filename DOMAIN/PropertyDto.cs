using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN
{
    public class PropertyDto
    {

        public string ID { get; set; } = Guid.NewGuid().ToString();

        public string OwnerID { get; set; }
        public string PropertyName { get; set; }
        public string OwnerName { get; set; }

        public List<MapPointDto>? Vertices { get; set; } = new List<MapPointDto>();

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
