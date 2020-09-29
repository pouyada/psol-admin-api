using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace PsolAdminApi.Areas.Core.Options
{
    public class JwtSettings
    {
        public string Secret { get; set; }
    }
}
