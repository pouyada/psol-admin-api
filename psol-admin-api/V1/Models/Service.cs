using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PsolAdminApi.V1.Models
{
    public class Service
    {
        private string RequestUri;
        private string Method;
        private HttpClient Client;
        private HttpRequestMessage RequestMessage;
        private HttpResponseMessage Response;
        private string ApiResponse;

        public Service(string method) 
        {
            RequestUri = "http://localhost:21111/api/GatewayController/Execute";
            Method = method;
            Client = new HttpClient();
            RequestMessage = new HttpRequestMessage(new HttpMethod(Method), RequestUri);
            ApiResponse = null;
        }

        public void SetRequestMessage (ExecuteGateway gatewayExecute)
        {
            var json = JsonConvert.SerializeObject(gatewayExecute);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            RequestMessage.Content = stringContent;
        }

        public string GetResponseMessage()
        {
            Response = Client.SendAsync(RequestMessage).Result;
            ApiResponse = Response.Content.ReadAsStringAsync().Result;
            try
            {
                if (ApiResponse != "")
                    //return JsonConvert.DeserializeObject<List<PackageDTO>>(apiResponse);
                    return ApiResponse.ToString();
                else
                    throw new Exception();
            }   
            catch (Exception)
            {
                throw new Exception($"An error ocurred while calling the API. It responded with the following message: {Response.StatusCode} {Response.ReasonPhrase}");
            }
        }

    }
}
