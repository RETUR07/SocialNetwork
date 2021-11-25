using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Security.DTO;
using System;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private IAuthService _authorizationService;

        public AuthorizationController(IAuthService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticateRequest model)
        {
            try
            {
                var response = await _authorizationService.AuthenticateAsync(model, ipAddress());
                setTokenCookie(response.RefreshToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _authorizationService.RefreshTokenAsync(refreshToken, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeTokenAsync(RevokeTokenRequest model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            await _authorizationService.RevokeTokenAsync(token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }

        [HttpGet("{id}/refresh-tokens")]
        public async Task<IActionResult> GetRefreshTokensAsync(int id)
        {
            var refreshTokens = await _authorizationService.GetUserRefreshTokensAsync(id);
            return Ok(refreshTokens);
        }

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
