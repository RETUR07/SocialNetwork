using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SocialNetwork.Security.Settings;
using SocialNetworks.Repository.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Security.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IRepositoryManager userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                context.Items["User"] = await userService.User.GetUserAsync(userId.Value, false);
            }

            await _next(context);
        }
    }
}
