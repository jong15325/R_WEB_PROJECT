namespace R_WEB_PROJECT.Utilities.Manager
{
    public class UserInfoManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInfoManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string GetUserIPAddress()
        {
            string? ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            return ipAddress;
        }

        public string GetUserAgent()
        {
            string? userAgent = _httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString();
            return userAgent;
        }
    }
}
