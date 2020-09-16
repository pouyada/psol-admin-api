using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsolAdminApi.V1.Models
{
    public class ExecuteGateway
    {
        public string NomeComponente { get; set; }
        public string NomeOperazione { get; set; }
        public string Country { get; set; }
        public string Token { get; set; }
        public JObject JsonInput { get; set; }

    }
}
