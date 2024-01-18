using Microsoft.Extensions.Caching.Distributed;
using R_WEB_PROJECT.Services.Abstraction.Login;
using R_WEB_PROJECT.Services.Implementation.Login;
using R_WEB_PROJECT.Utilities.Redis.Session;

namespace R_WEB_PROJECT.Modules.Session
{
    public static class RedisSessionModule
	{
		public static void Register(IServiceCollection services)
		{
			services.AddScoped<RedisSessionStore>();

			//계정 세션
			services.AddSession(options =>
			{
				options.Cookie.Name = "AccountSession";
				options.IdleTimeout = TimeSpan.FromMinutes(30); // 세션 유지 시간 설정
				options.Cookie.HttpOnly = true; //세션 쿠키가 JavaScript를 통해 액세스되지 못하도록 하는 옵션
				options.Cookie.IsEssential = true; // 세션 쿠키가 필수적인지를 나타내는 옵션
			});
		}
	}
}
