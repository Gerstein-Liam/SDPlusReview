using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Models;

namespace WPF.Services.HttpServices.Backend
{
 
    public interface IBackendHTTPService
    {
        Task<List<Owner>> GetAllOwner();

        Task<string> PostAllOwner(List<Owner> owners);       //Une rustine comme je n'aurai pas le temps d'implenter des mises a jour(put/delete) individuel

        Task<string> DeleteOwner(Owner owner);               //TODO

        Task<string> UpdateOwner(Owner owner);               //TODO

        Task<string> DeleteProperty(Property property);      //TODO

        Task<string> UpdateProperty(Property property);     //TODO
    }
}
