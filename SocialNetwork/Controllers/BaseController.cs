using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SocialNetwork.Controllers
{
    [ApiController]
    public class Base : ControllerBase
    {
        public readonly int UserId;

        public Base()
        {
            var iter = HttpContext.User.Claims.GetEnumerator();
            while (iter.Current.Type != ClaimTypes.UserData && iter.MoveNext()) ;
            UserId = int.Parse(iter.Current.Value);
        }
    }
}
