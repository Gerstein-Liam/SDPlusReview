using DOMAIN;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

namespace WPF.Services.HttpServices.Backend
{
 
    public class BackendHTTPService : IBackendHTTPService
    {

        private readonly IModelsMapper _mapper;
        private readonly string _baseUri;

        private readonly string _apiBaseUri;

        public BackendHTTPService(string apiBaseUri, IModelsMapper mapper, ILogger<BackendHTTPService> logger)
        {
            _mapper = mapper;

            _apiBaseUri = apiBaseUri;
            logger.LogInformation("Hello");


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
                List<OwnerDto> ownerDtoList = await client.Get_Async<List<OwnerDto>>("owner");
                List<Owner> ownerList = _mapper.map(ownerDtoList);
                if (ownerList == null)
                {
                    throw new Exception("The Get All Request Failed");
                }
                return ownerList;
            }
        }

        public async Task<string> PostAllOwner(List<Owner> owners)
        {
            List<OwnerDto> ownerListDto = _mapper.map(owners);
            using (BackendHTTPClient client = new BackendHTTPClient())
            {
           

                return await client.Post_Async("owner", owners);

              
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
