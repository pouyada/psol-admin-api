using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PsolAdminApi.Areas.Core.Models;
using PsolAdminApi.Areas.Core.Services;
using PsolAdminApi.V1.Models;

namespace PsolAdminApi.Areas.Core.Controllers
{
    [Area("core")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly HttpService _httpService;
        private readonly string _country;

        public PackagesController(HttpService httpService, UtilService utilService)
        {
            _httpService = httpService;
            _country = utilService.GetCountryFromRequest();
        }

        // GET: api/Package
        [HttpGet]
        public ActionResult<List<PackageDTO>> Get()
        {
            // var packageList = Packages();
            return Ok("Core Controller");

            //return Enumerable.Range(0, 1).Select(index => new Package
            //{
            //    Name = "snb"
            //})
            //.ToArray();
        }

        [NonAction]
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
        public ActionResult Post([FromBody]IdPackageDTO idPackage)
        {
            try
            {
                var packageList = GetPackageById(idPackage);
                return Ok(packageList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [NonAction]
        public string GetPackageById(IdPackageDTO idPackage)
        {
            string apiResponse = null;
            List<PackageDTO> packageList = new List<PackageDTO>();
            var gatewayExecute = new GatewayRequestModel("Package", "GetPkgPackageById", _country, (JObject)JToken.FromObject(idPackage));

            _httpService.SetMethod("POST");         
            _httpService.SetRequestMessage(gatewayExecute);
            apiResponse = _httpService.GetResponseMessage();
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
