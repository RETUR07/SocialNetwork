using Microsoft.AspNetCore.SignalR;

namespace SocialNetwork.Hubs
{
    public class BaseHub : Hub
    {
        public int UserId
        {
            get
            {
                var claimIdentity = Context.User.Identities.FirstOrDefault(x => x.Claims.
                    FirstOrDefault(x => x.Type == ClaimTypes.UserData) != null);
                return int.Parse(claimIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData).Value);
            }
        }
    }
}
