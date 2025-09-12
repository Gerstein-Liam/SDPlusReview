using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class Owner
    {
        public string ID { get; set; } = string.Empty;

        public string OwnerName { get; set; } = string.Empty;

        public List<Property> Properties { get; set; }

        public Owner()
        {
            Properties = new List<Property>();

        }
    }
}
