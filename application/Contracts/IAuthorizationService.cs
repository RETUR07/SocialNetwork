using SocialNetwork.Entities.SecurityModels;
using SocialNetwork.Security.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Contracts
{
    public interface IAuthorizationService
    {
        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        public AuthenticateResponse RefreshToken(string token, string ipAddress);
        public void RevokeToken(string token, string ipAddress);
        public Task<List<RefreshToken>> GetUserRefreshTokensAsync(int id);
    }
}
