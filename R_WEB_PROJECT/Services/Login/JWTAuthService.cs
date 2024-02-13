using R_WEB_PROJECT.Models.User;
using System.Text.Json;
using System.Text;
using R_WEB_PROJECT.Utilities.Log;
using R_WEB_PROJECT.DTOs;
using System.Text.Json.Nodes;
using Microsoft.IdentityModel.Tokens;
using static R_WEB_PROJECT.Utilities.Enums.RoleEnum;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace R_WEB_PROJECT.Services.Login
{
    public interface IJWTAuthService
    {
        Task<string> AuthenticateUserAsync(AccountValidDTO dto);
    }

    public class JWTAuthService : IJWTAuthService
    {
        private readonly IHttpClientFactory _clientFactory;

        public JWTAuthService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<string> AuthenticateUserAsync(AccountValidDTO dto)
        {
            try
            {
                LogUtil.Debug("SYSTEM", "=============================== AuthenticateUserAsync Service Start ===============================");

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                     new Claim(ClaimTypes.NameIdentifier, UserId),
                     new Claim(ClaimTypes.Role, UserRoleCd)

                 };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpirationMinutes"])),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);

                LogUtil.Debug("SYSTEM", $"JWT 인증 토큰3 : " + token);

                return token;
            }
            catch (Exception ex)
            {
                // API 호출 실패 처리
                LogUtil.Error("SYSTEM", $"An error occurred during AuthenticateUserAsync Service : {ex.GetType().Name} - {ex.Message}", ex);
                throw;
            }
            finally
            {
                LogUtil.Debug("SYSTEM", "=============================== AuthenticateUserAsync Service End ===============================");
            }
        }
    }
}
