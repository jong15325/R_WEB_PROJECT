using JWTAuthAPI.Services;

namespace JWTAuthAPI.Modules.ServiceModule
{
    public static class ServiceModule
	{
		public static void Register(IServiceCollection services)
		{
            //사용자 인증 토큰 생성 반환
            services.AddScoped<JWTAuthService>();
            
        }
	}
}
