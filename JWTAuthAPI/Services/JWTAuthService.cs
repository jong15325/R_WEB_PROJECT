using JWTAuthAPI.Utilities.Log;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthAPI.Services
{
    public interface IJWTAuthService
    {
        string GenerateToken(string userId, string role);
    }

    public class JWTAuthService : IJWTAuthService
    {
        private readonly IConfiguration _configuration;

        public JWTAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string UserId, string UserRoleCd)
        {
            try
            {
                LogUtil.Debug("API-JWT_AUTH", "=============================== GenerateToken Service Start ===============================");

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
            }
            catch (Exception ex)
            {
                LogUtil.Error("API-JWT_AUTH", $"An error occurred during GenerateToken Service: {ex.GetType().Name} - {ex.Message}", ex);
                throw;
            }
            finally
            {
                LogUtil.Debug("API-JWT_AUTH", "=============================== GenerateToken Service End ===============================");
            }
        }
    }
}
