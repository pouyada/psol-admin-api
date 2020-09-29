using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PsolAdminApi.Areas.Core.Models;
using PsolAdminApi.Areas.Core.Services;

namespace PsolAdminApi.Areas.Core.Controllers
{
    [Area("core")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly GatewayService _httpService;

        public PackagesController(GatewayService httpService, UtilService utilService)
        {
            _httpService = httpService;
        }

        // GET: api/Package
        [HttpGet]
        public async Task<ActionResult<List<PackageDTO>>> Get()
        {
            var packages = await GetAllPackages();
            return Ok(packages);
        }


        // Post: api/Package
        [HttpPost]
        public ActionResult Post([FromBody]string idPackage)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        // GET: api/Package/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<PackageDTO>> Get(int id)
        {
            var package = await GetPackageById(id);
            return Ok(package);
        }


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

        [NonAction]
        public async Task<PackageDTO> GetPackageById(int packageId)
        {
            JObject requestData = new JObject();
            requestData.Add ("idP", packageId);

            var gatewayRequestModel = new GatewayRequestModel("Package", "GetPkgPackageById", requestData);
            gatewayRequestModel.DataField = "packagesP";
            JObject apiResponse = await _httpService.Send(gatewayRequestModel);

            return JsonConvert.DeserializeObject<PackageDTO>(apiResponse.ToString());
        }

        [NonAction]
        public async Task<List<PackageDTO>> GetAllPackages()
        {
            //JObject requestData = new JObject();
            //requestData.Add("idP", packageId);

            var gatewayRequestModel = new GatewayRequestModel("Package", "GetPkgPackages", null);
            gatewayRequestModel.DataField = "packagesP";
            gatewayRequestModel.FetchEntity = false;
            JObject apiResponse = await _httpService.Send(gatewayRequestModel);

            return JsonConvert.DeserializeObject<List<PackageDTO>>(apiResponse["data"].ToString());
        }
    }
}
