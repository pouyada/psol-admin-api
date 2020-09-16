using Microsoft.AspNetCore.Mvc;
using PsolAdminApi.Areas.Italy.Models;
using System.Collections.Generic;

namespace PsolAdminApi.Areas.Italy.Controllers
{
    [Area("italy")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<PackageDTO>> Get()
        {
            return Ok("italian controller");
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