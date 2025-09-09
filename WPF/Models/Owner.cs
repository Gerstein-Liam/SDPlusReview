using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class Owner
    {
        public string ID { get; set; }

        public string OwnerName { get; set; }

        public List<Property> Properties { get; set; }

        public Owner()
        {
            Properties = new List<Property>();

        }
    }
}
