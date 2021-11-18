using AutoMapper;
using Microsoft.Extensions.Options;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Entities.Models;
using SocialNetwork.Entities.SecurityModels;
using SocialNetwork.Security.Authorization;
using SocialNetwork.Security.DTO;
using SocialNetwork.Security.Settings;
using SocialNetworks.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SocialNetwork.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;


        public UserService(IRepositoryManager repository, IJwtUtils jwtUtils, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _repository = repository;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public async Task AddFriendAsync(int userId, int friendId)
        {
            var user = await _repository.User.GetUserAsync(userId, true);
            var friend = await _repository.User.GetUserAsync(friendId, true);

            if (user == null || friend == null) return;
            if (user.Friends.Contains(friend) || friend.Friends.Contains(user)) return;

            if(user.Subscribers.Contains(friend))
            {
                user.Friends.Add(friend);
                user.Subscribers.Remove(friend);
            }
            else
            {
                friend.Subscribers.Add(user);
            }
            await _repository.SaveAsync();
        }

        public async Task DeleteFriendAsync(int userId, int friendId)
        {
            var user = await _repository.User.GetUserAsync(userId, true);
            var friend = await _repository.User.GetUserAsync(friendId, true);

            if (user == null || friend == null) return;

            if(friend.Subscribers.Contains(user))
            {
                friend.Subscribers.Remove(user);
            }
            if(user.Friends.Contains(friend) || friend.Friends.Contains(user))
            {
                user.Friends.Remove(friend);
                friend.Friends.Remove(user);

                user.Subscribers.Add(friend);
            }
            await _repository.SaveAsync();
        }

        public async Task<User> CreateUserAsync(UserRegistrationForm userdto)
        {
            if (userdto == null)
            {
                return null;
            }

            if (_repository.User.FindByCondition(u => u.Username == userdto.Username, false).Count() != 0) return null;        
            var user = _mapper.Map<User>(userdto);
            user.PasswordHash = BCryptNet.HashPassword(userdto.Password);
            _repository.User.Create(user);
            await _repository.SaveAsync();
            return user;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _repository.User.GetUserAsync(userId, true);
            _repository.User.Delete(user);
            await _repository.SaveAsync();
        }

        public async Task<UserForResponseDTO> GetUserAsync(int userId)
        {
            var user = await _repository.User.GetUserAsync(userId, false);
            if (user == null)
            {
                return null;
            }
            var userdto = _mapper.Map<UserForResponseDTO>(user);
            return userdto;
        }

        public async Task<IEnumerable<UserForResponseDTO>> GetUsersAsync()
        {
            var users = await _repository.User.GetAllUsersAsync(false);
            var usersdto = _mapper.Map<IEnumerable<UserForResponseDTO>>(users);
            return usersdto;
        }

        public async Task<bool> UpdateUserAsync(int userId, UserForm userdto)
        {
            if (userdto == null)
            {
                return false;
            }

            var user = await _repository.User.GetUserAsync(userId, true);

            if (user == null)
            {
                return false;
            }

            _mapper.Map(userdto, user);
            await _repository.SaveAsync();
            return true;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var user = _repository.User.FindByCondition(x => x.Username == model.Username, true).SingleOrDefault();

            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new Exception("Username or password is incorrect");

            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            user.RefreshTokens.Add(refreshToken);

            removeOldRefreshTokens(user);

            _repository.SaveAsync();

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                revokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                _repository.User.Update(user);
                _repository.SaveAsync();
            }

            if (!refreshToken.IsActive)
                throw new Exception("Invalid token");

            var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            removeOldRefreshTokens(user);

            _repository.User.Update(user);
            _repository.SaveAsync();

            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        public void RevokeToken(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new Exception("Invalid token");

            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            _repository.User.Update(user);
            _repository.SaveAsync();
        }

        private User getUserByRefreshToken(string token)
        {
            var user = _repository.User.FindByCondition(u => u.RefreshTokens.Any(t => t.Token == token), false).SingleOrDefault();

            if (user == null)
                throw new Exception("Invalid token");

            return user;
        }

        private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private void removeOldRefreshTokens(User user)
        {
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
        {
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    revokeRefreshToken(childToken, ipAddress, reason);
                else
                    revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        public async Task<IEnumerable<RefreshToken>> GetUserRefreshTokensAsync(int id)
        {
            var user = await _repository.User.GetUserAsync(id, false);
            if (user == null || user.RefreshTokens == null) return null;
            return user.RefreshTokens;
        }
    }
}
