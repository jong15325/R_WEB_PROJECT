using R_WEB_PROJECT.Repositories.Log;
using R_WEB_PROJECT.Repositories.Login;

namespace R_WEB_PROJECT.Modules.ServiceModule
{
    public static class RepositoryModule
	{
		public static void Register(IServiceCollection services)
		{
			//로그인
			services.AddScoped<ILoginRepository, LoginRepository>();

            //로그인 로그
            services.AddScoped<ILogLoginRepository, LogLoginRepository>();
        }
	}
}
