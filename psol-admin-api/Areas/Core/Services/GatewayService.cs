﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PsolAdminApi.Areas.Core.Models;
using PsolAdminApi.Areas.Core.Models.Request;
using PsolAdminApi.Areas.Core.Models.Response;
using PsolAdminApi.Areas.Core.Options;
using PsolAdminApi.Areas.Core.Utility;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace PsolAdminApi.Areas.Core.Services
{
    public class GatewayService
    {
        private string _country;
        private readonly HttpClient _httpClient;
        private string apiPath;
        private string authPath;

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public GatewayService(UtilService utilService, HttpClient client, IOptions<EnvSettings> env)
        {
            _httpClient = client;
            _country = utilService.GetCountryFromRequest();
            apiPath = env.Value.GATEWAY_API_PATH;
            authPath = env.Value.GATEWAY_AUTH_PATH;
        }

        public async Task<JObject> Send(GatewayRequestModel requestModel)
        {

            // TODO
            // Use cancelToekn
            // Add logs

            requestModel.Country = _country;
            var httpContent = ConvertToStringContent(requestModel);

            var response = await _httpClient.PostAsync(apiPath, httpContent);
            var stream = await response.Content.ReadAsStreamAsync();

            CheckRequestStatus(response, stream);

            var responseContent = DeserializeJsonFromStream<JObject>(stream);

            if (requestModel.DataField != null)
            {
                var responseData = responseContent[requestModel.DataField];

                if (responseData.GetType() == typeof(JArray))
                {
                    if (requestModel.FetchEntity)
                        return (JObject)responseData[0];
                    else
                    {
                        JObject resObj = new JObject();
                        resObj.Add("data", responseData);
                        return resObj;
                    }
                       
                }
            }

            return responseContent;
        }

        public async Task<LoginGatewayResponseModel> SendAuthRequest(LoginGatewayRequestModel requestModel)
        {
            var loginResponseModel = new LoginGatewayResponseModel();
            loginResponseModel.Success = false;

            var httpContent = ConvertToStringContent(requestModel);

            var response = await _httpClient.PostAsync(authPath, httpContent);
            var stream = await response.Content.ReadAsStreamAsync();

            if (!response.IsSuccessStatusCode)
            {
                //write in logs
                return loginResponseModel;
            }

            var responseObject = DeserializeJsonFromStream<JObject>(stream);
            var resultCode = (int?)responseObject.SelectToken("Result");

            if (resultCode == 2 || resultCode == 5)
            {
                loginResponseModel.Message = responseObject.SelectToken("Message").ToString();
                return loginResponseModel;
            }

            var userInfo = responseObject.SelectToken("UserInfo");
            loginResponseModel.Success = true;
            loginResponseModel.Username = userInfo.SelectToken("Username").ToString();
            loginResponseModel.Roles = userInfo.SelectToken("Roles").Values<string>();
            loginResponseModel.FirstName = userInfo.SelectToken("UserData").SelectToken("Name").ToString();
            loginResponseModel.LastName = userInfo.SelectToken("UserData").SelectToken("Surname").ToString(); ;
            loginResponseModel.Email = userInfo.SelectToken("UserData").SelectToken("Email").ToString(); ;

            return loginResponseModel;
        }

        private static async void CheckRequestStatus(HttpResponseMessage response, Stream stream)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await StreamToStringAsync(stream);
                throw new ApiException
                {
                    StatusCode = (int)response.StatusCode,
                    Content = content
                };
            }
        }
        private static StringContent ConvertToStringContent<T>(T requestModel)
        {
            var jsonData = JsonConvert.SerializeObject(requestModel);
            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return httpContent;
        }

        private static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var jr = new JsonSerializer();
                var searchResult = jr.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        private static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
            {
                using (var sr = new StreamReader(stream))
                {
                    content = await sr.ReadToEndAsync();
                }
            }

            return content;
        }
        

    }
}
