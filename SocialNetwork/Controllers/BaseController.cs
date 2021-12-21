using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace SocialNetwork.Controllers
{
    [ApiController]
    public class Base : ControllerBase
    {
        public readonly int UserId;

        public Base()
        {
            var claimIdentity = HttpContext.User.Identities.FirstOrDefault(x => x.Claims.
                    FirstOrDefault(x => x.Type == ClaimTypes.UserData) != null);
            UserId = int.Parse(claimIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData).Value);
        }
    }
}
