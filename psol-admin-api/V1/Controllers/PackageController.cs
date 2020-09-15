using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PsolAdminApi.Models;
using PsolAdminApi.V1.Models;

namespace PsolAdminApi.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        // GET: api/Package
        [HttpGet]
        public Task<List<PackageDTO>> Get()
        {
            var packageList = Packages();
            return packageList;
            //return Enumerable.Range(0, 1).Select(index => new Package
            //{
            //    Name = "snb"
            //})
            //.ToArray();
        }

        public async Task<List<PackageDTO>> Packages()
        {
            string apiResponse = null;
            List<PackageDTO> packageList = new List<PackageDTO>();
            using (var httpClient = new HttpClient())
            {
                //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer PSolDeApp01");
                //httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                //httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.26.3");
                //httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
                //httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                //httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                //httpClient.DefaultRequestHeaders.Add("RootOrg", "PSOLDE");
                //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
                try
                {
                    using (var response = await httpClient.GetAsync("http://localhost:21111/psol/1.0/services"))
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                        packageList = JsonConvert.DeserializeObject<List<PackageDTO>>(apiResponse);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return packageList;
        }

        // Post: api/Package
        [HttpPost]
        public string Post([FromBody]IdPackageDTO idPackage)
        {
            var packageList = GetPackageById(idPackage);
            return packageList;
        }

        public string GetPackageById(IdPackageDTO idPackage)
        {
            string apiResponse = null;
            List<PackageDTO> packageList = new List<PackageDTO>();
            var gatewayExecute = new ExecuteGateway();
            gatewayExecute.NomeComponente = "Package";
            gatewayExecute.NomeOperazione = "GetPkgPackageById";
            gatewayExecute.Country = "Germany";
            gatewayExecute.Token = string.Empty;
            gatewayExecute.JsonInput = (JObject)JToken.FromObject(idPackage);

            var service = new Service("POST");
            service.SetRequestMessage(gatewayExecute);
            apiResponse = service.GetResponseMessage();
            return apiResponse;          
        }

        // GET: api/Package/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        //// POST: api/Package
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT: api/Package/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
