using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using psol_admin_api.Models;

namespace psol_admin_api.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        // GET: api/Package
        [HttpGet]
        public IEnumerable<Package> Get()
        {
            return Enumerable.Range(0, 1).Select(index => new Package
            {
                Name = "snb"
            })
            .ToArray();
        }

        // GET: api/Package/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Package
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
    }
}
