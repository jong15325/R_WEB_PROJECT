using R_WEB_PROJECT.Services.Log;
using R_WEB_PROJECT.Services.Login;

namespace R_WEB_PROJECT.Modules.ServiceModule
{
    public static class ServiceModule
	{
		public static void Register(IServiceCollection services)
		{
			//로그인
			services.AddScoped<ILoginService, LoginService>();

            //로그인
            services.AddScoped<ILogLoginService, LogLoginService>();
        }
	}
}
