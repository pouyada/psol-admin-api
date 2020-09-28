using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PsolAdminApi.Areas.Core.Models.Request;
using PsolAdminApi.Areas.Core.Models.Response;
using PsolAdminApi.Areas.Core.Services;

namespace PsolAdminApi.Areas.Core.Controllers
{
    [Area("core")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GatewayService _httpService;

        public AuthController(GatewayService httpService)
        {
            _httpService = httpService;
        }

        [HttpPost]
        [Route("generate_token")]
        public async Task<IActionResult> GenerateToken([FromBody] TokenRequestModel request)
        {
            var authResponse = await LoginAsync(request);

            //if (!authResponse.Success)
            //{
            //    return BadRequest(new AuthFailedResponse
            //    {
            //        Errors = authResponse.Errors
            //    });
            //}

            //return Ok(new AuthSuccessResponse
            //{
            //    Token = authResponse.Token,
            //    RefreshToken = authResponse.RefreshToken
            //});
            return Ok();
        }

        [NonAction]
        public async Task<LoginGatewayResponseModel> LoginAsync(TokenRequestModel request)
        {
            var loginGatewayRequestModel = new LoginGatewayRequestModel();
            loginGatewayRequestModel.RootOrg = "PSOLDE";
            loginGatewayRequestModel.UserType = 1;
            loginGatewayRequestModel.Username = request.Username;
            loginGatewayRequestModel.Password = request.Password;

            JObject apiResponse = await _httpService.SendAuthRequest(loginGatewayRequestModel);

            return JsonConvert.DeserializeObject<LoginGatewayResponseModel>(apiResponse.ToString());
        }
    }
}