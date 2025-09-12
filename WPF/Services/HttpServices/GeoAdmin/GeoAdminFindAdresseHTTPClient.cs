using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WPF.Services.HttpServices.GeoAdmin
{
    public class GeoAdminFindAdresseHTTPClient : HttpClient
    {
        public GeoAdminFindAdresseHTTPClient()
        {
            BaseAddress = new Uri("https://api3.geo.admin.ch/rest/services/api/");
        }
        public async Task<T> Get_Async<T>(string adress)
        {

            string query = $"SearchServer?type=locations&sr=3857&searchText=${adress}";
            return await this.GetFromJsonAsync<T>(query);
        }
      
    }
}
