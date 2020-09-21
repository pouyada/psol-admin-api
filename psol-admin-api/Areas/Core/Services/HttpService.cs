using Newtonsoft.Json;
using PsolAdminApi.V1.Models;
using System;
using System.Net.Http;
using System.Text;

namespace PsolAdminApi.Areas.Core.Services
{
    public class HttpService
    {
        private string RequestUri;
        private string Method;
        private HttpClient Client;
        private HttpRequestMessage RequestMessage;
        private HttpResponseMessage Response;
        private string ApiResponse;

        public HttpService()
        {
            RequestUri = "http://localhost:21111/api/GatewayController/Execute";            
            Client = new HttpClient();
            ApiResponse = null;
        }

        public HttpService(string method) 
        {
            RequestUri = "http://localhost:21111/api/GatewayController/Execute";
            Method = method;
            Client = new HttpClient();
            RequestMessage = new HttpRequestMessage(new HttpMethod(Method), RequestUri);
            ApiResponse = null;
        }

        public void SetMethod(string method)
        {
            Method = method;
            RequestMessage = new HttpRequestMessage(new HttpMethod(Method), RequestUri);
        }

        public void SetRequestMessage (GatewayRequestModel gatewayExecute)
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
