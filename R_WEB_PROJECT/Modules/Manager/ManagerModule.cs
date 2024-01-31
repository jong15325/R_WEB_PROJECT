using R_WEB_PROJECT.Repositories.Log;
using R_WEB_PROJECT.Repositories.Login;
using R_WEB_PROJECT.Utilities.Manager;

namespace R_WEB_PROJECT.Modules.Manager
{
    public static class ManagerModule
    {
        public static void Register(IServiceCollection services)
        {
            //리소스 메세지 매니저 싱글톤 등록
            services.AddSingleton<ResourceManager>();

            //유저 정보 매니저 등록
            services.AddScoped<UserInfoManager>();
        }
    }
}
