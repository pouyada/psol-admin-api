using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsolAdminApi.Areas.Core.Services
{
    public class UtilService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private Dictionary<string, string> countries;

        public UtilService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            countries = new Dictionary<string, string>()
            {
                {"AT", "Austria"},
                {"IT", "Italy"},
                {"DE", "Germany"}
            };
        }

        public string GetCountryFromRequest()
        {
            var country = _contextAccessor.HttpContext.Request.Headers["Country"];
            return countries[country];
        }
    }
}
