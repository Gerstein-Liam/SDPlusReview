using Newtonsoft.Json;
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

namespace WPF.Services.HttpServices
{
    public class BackendHTTPClient : HttpClient
    {
        public BackendHTTPClient()
        {
            this.BaseAddress = new Uri("https://localhost:7124/api/");
        }
        public async Task<T?> Get_Async<T>(string uri)
        {
            return await this.GetFromJsonAsync<T>(uri);
        }

        public async Task<string> Post_Async<T>(string uri, T Body) {

            var myContent = JsonConvert.SerializeObject(Body,Formatting.None);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            using HttpResponseMessage response = await PostAsync(uri, byteContent);
            

            return response.StatusCode.ToString();


        }
    }
}
