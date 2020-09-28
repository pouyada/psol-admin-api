using System.Collections.Generic;

namespace PsolAdminApi.Areas.Core.Models.Response
{
    public class TokenResponseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginGatewayResponseModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

    }


}
