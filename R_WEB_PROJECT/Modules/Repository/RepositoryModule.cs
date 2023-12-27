using R_WEB_PROJECT.Repositories.Abstraction.Login;
using R_WEB_PROJECT.Repositories.Implementation.Login;

namespace R_WEB_PROJECT.Modules.ServiceModule
{
	public static class RepositoryModule
	{
		public static void Register(IServiceCollection services)
		{
			//로그인
			services.AddScoped<ILoginRepository, LoginRepository>();
		}
	}
}
