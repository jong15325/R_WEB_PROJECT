using R_WEB_PROJECT.Services.Abstraction.Login;
using R_WEB_PROJECT.Services.Implementation.Login;

namespace R_WEB_PROJECT.Modules.ServiceModule
{
	public static class ServiceModule
	{
		public static void Register(IServiceCollection services)
		{
			//로그인
			services.AddScoped<ILoginService, LoginService>();
		}
	}
}
