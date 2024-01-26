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
            var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress;
            if (ipAddress != null && ipAddress.IsIPv4MappedToIPv6)
            {
                ipAddress = ipAddress.MapToIPv4();
            }
            return ipAddress.ToString();
        }

        public string GetUserAgent()
        {
            string? userAgent = _httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString();
            return userAgent;
        }
    }
}
