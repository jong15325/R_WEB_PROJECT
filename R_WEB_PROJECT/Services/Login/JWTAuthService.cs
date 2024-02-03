using R_WEB_PROJECT.Models.User;
using System.Text.Json;
using System.Text;
using R_WEB_PROJECT.Utilities.Log;
using R_WEB_PROJECT.DTOs;

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

                var client = new HttpClient();
                var json = JsonSerializer.Serialize(dto.AccountInfo);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // JWTAuthAPI 호출
                var response = await client.PostAsync("https://localhost:7077/api/login", content);
                response.EnsureSuccessStatusCode();

                var token = await response.Content.ReadAsStringAsync();
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
