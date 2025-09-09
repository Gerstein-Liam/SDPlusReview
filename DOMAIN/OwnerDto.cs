using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN
{
    public class OwnerDto
    {


        public string ID { get; set; } = Guid.NewGuid().ToString();

        public string OwnerName { get; set; }

        public List<PropertyDto>? Properties { get; set; } = new List<PropertyDto>();

        public OwnerDto()
        {

        }

        public OwnerDto(string ownerName, List<PropertyDto>? properties)
        {
            OwnerName = ownerName;
            Properties = properties;
        }
    }
}
