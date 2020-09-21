using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PsolAdminApi.Areas.Core.Middleware
{
    public class CountryMiddleware
    {
        private readonly RequestDelegate _next;

        public CountryMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string[] countries = { "AT", "DE", "IT" };

            string token = httpContext.Request.Headers["Country"];

            if (!countries.Contains(token))
            {
                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response
                               .WriteAsync(JsonConvert.SerializeObject(new { error = "Missing Country in the Headers", success = false, origin = httpContext.Request.Path }))
                               .ConfigureAwait(false);
                return;
            }

            await _next(httpContext);
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CountryMiddleware>();
        }
    }
}
