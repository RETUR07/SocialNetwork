using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace SocialNetwork.Controllers
{
    [ApiController]
    public class Base : ControllerBase
    {
        public int UserId
        {
            get
            {
                var claimIdentity = HttpContext.User.Identities.FirstOrDefault(x => x.Claims.
                    FirstOrDefault(x => x.Type == ClaimTypes.UserData) != null);
                return int.Parse(claimIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData).Value);
            }
        }     
    }
}
