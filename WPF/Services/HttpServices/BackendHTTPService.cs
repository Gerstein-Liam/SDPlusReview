using ABI.Windows.ApplicationModel.Activation;
using DOMAIN;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WPF.Models;
using WPF.Models.Mappers;

namespace WPF.Services.HttpServices
{
 
    public class BackendHTTPService : IBackendHTTPService
    {

        private readonly IModelsMapper _mapper;

        public BackendHTTPService(IModelsMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<string> DeleteOwner(Owner owner)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteProperty(Property property)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Owner>> GetAllOwner()
        {
            using (BackendHTTPClient client = new BackendHTTPClient())
            {
                string uri = $"api/owner";
                List<OwnerDto>? ownerDtoList = await client.Get_Async<List<OwnerDto>>("owner");
                if (ownerDtoList == null) throw new NullReferenceException();
                List<Owner> ownerList = _mapper.map(ownerDtoList);
                return ownerList;
            }
        }

        public async Task<string> PostAllOwner(List<Owner> owners)
        {
            List<OwnerDto> ownerListDto = _mapper.map(owners);
            using (BackendHTTPClient client = new BackendHTTPClient())
            { 
                string r= await client.Post_Async("owner", JsonConvert.SerializeObject(ownerListDto));
                return r;
              
            }
        }

        public Task<string> UpdateOwner(Owner owner)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateProperty(Property property)
        {
            throw new NotImplementedException();
        }
    }
}
