using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsolAdminApi.Areas.Core.Utility
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public string Content { get; set; }
    }
}
