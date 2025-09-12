using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace WPF.Services.HttpServices.Backend
{
    public class BackendHTTPClient : HttpClient
    {
        public BackendHTTPClient()
        {
            BaseAddress = new Uri("https://localhost:7124/api/");
        }
        public async Task<T> Get_Async<T>(string uri)
        {
            return await this.GetFromJsonAsync<T>(uri);
        }
        public async Task<string> Post_Async<T>(string uri, T Body)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(Body), Encoding.Default, "application/json");
            jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            jsonContent.Headers.ContentType.CharSet = "";
            using HttpResponseMessage response = await this.PostAsJsonAsync(uri, Body);
            return "OK";
        }
    }
}