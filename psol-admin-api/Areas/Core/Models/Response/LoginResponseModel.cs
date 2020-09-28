using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsolAdminApi.Areas.Core.Models.Response
{
    public class TokenResponseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginGatewayResponseModel
    {
        public string RootOrg { get; set; }
        public int UserType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Success{ get; set; }
    }
}
