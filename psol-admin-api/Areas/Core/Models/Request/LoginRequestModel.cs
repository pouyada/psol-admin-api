namespace PsolAdminApi.Areas.Core.Models.Request
{
    public class TokenRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginGatewayRequestModel
    {
        public string RootOrg { get; set; }
        public int UserType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
