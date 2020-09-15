using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PsolAdminApi.V1.Italy.Controllers
{
    //[Route("v{version:apiVersion}/{country}/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
            //var packageList = Packages();
            //return packageList;
            //return Enumerable.Range(0, 1).Select(index => new Package
            //{
            //    Name = "snb"
            //})
            //.ToArray();
        }
    }
}