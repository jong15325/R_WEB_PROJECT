using JWTAuthAPI.Services;
using JWTAuthAPI.Utilities.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JWTAuthAPI.Models.User;

namespace JWTAuthAPI.Controllers
{
    [ApiController]
    public class JWTAuthController : Controller
    {
        private readonly JWTAuthService _jwtAuthService;

        public JWTAuthController(JWTAuthService jwtAuthService)
        {
            _jwtAuthService = jwtAuthService;
        }

        [HttpPost("/api/login")]
        public IActionResult Login([FromBody] AccountModel model)
        {
            try
            {
                LogUtil.Debug("API-JWT_AUTH", "=============================== Login Start ===============================");
                LogUtil.Info("API-JWT_AUTH", $"userIduserIduserId : {model.UserId}");
                LogUtil.Info("API-JWT_AUTH", $"rolerolerolerolerolerole : {model.UserRoleCd}");
                var token = _jwtAuthService.GenerateToken(model.UserId, model.UserRoleCd);

                LogUtil.Info("API-JWT_AUTH", $"tokentokentokentoken{token}");

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                LogUtil.Error("API-JWT_AUTH", $"An error occurred during login: {ex.GetType().Name} - {ex.Message}", ex);
                return StatusCode(500, "An error occurred during login. Please try again later.");
            }
            finally
            {
                LogUtil.Debug("API-JWT_AUTH", "=============================== Login End ===============================");
            }
        }

        [Authorize(Roles = "Admin")] // 예시로 Admin 역할만 허용하는 엔드포인트
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return Ok("Hello Admin!");
        }
    }
}
