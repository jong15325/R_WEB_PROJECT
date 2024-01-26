using Microsoft.EntityFrameworkCore;
using log4net.Config;
using log4net;
using System.Reflection;
using R_WEB_PROJECT.Modules.ServiceModule;
using R_WEB_PROJECT.Modules.Session;
using R_WEB_PROJECT.Utilities.Log;
using Microsoft.AspNetCore.Mvc.Razor;
using R_WEB_PROJECT.Resources;
using R_WEB_PROJECT.Utilities.Manager;
using R_WEB_PROJECT.Modules.Manager;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var serverType = configuration.GetValue<string>("ServerType");
GlobalContext.Properties["ServerType"] = serverType;
LogUtil.Info("SYSTEM", $"Server Type: {serverType}", "Program");

//로그
//Add log4net as logging provider
//logger.Info, Debug, Warn, Error, Fatal
//{0} : 첫번째 매개변수,$ 포함한 {count} 변수 count
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
//XmlConfigurator.Configure(logRepository, new FileInfo(serverType == "real" ? "log4net_real.config" : "log4net_dev.config"));
XmlConfigurator.Configure(logRepository, new FileInfo("log4net_dev.config"));
var _logger = LogManager.GetLogger(typeof(Program));
LogUtil.Info("SYSTEM", "Logger registered", "Program");

//의존성 주입
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//DB 셋팅
var connectionString = configuration.GetConnectionString(serverType == "REAL" ? "RealConnection" : "DevConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
LogUtil.Info("SYSTEM", $"Database connection string: {connectionString}", "Program");
builder.Services.AddScoped(provider => new DatabaseManager(connectionString));

//레디스 셋팅
var redisConnectionString = builder.Configuration.GetConnectionString(serverType == "REAL" ? "RealRedisConnection" : "DevRedisConnection");
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = redisConnectionString;
	options.InstanceName = "rw_"; // 선택적으로 인스턴스 이름 설정
});

//분산 메모리 캐시
builder.Services.AddDistributedMemoryCache();

//서비스 모듈 등록
ServiceModule.Register(builder.Services);
LogUtil.Info("SYSTEM", $"ServiceModule registered", "Program");

//레포지토리 모듈 등록
RepositoryModule.Register(builder.Services);
LogUtil.Info("SYSTEM", $"RepositoryModule registered", "Program");

//레디스 세션 모듈 등록
RedisSessionModule.Register(builder.Services);
LogUtil.Info("SYSTEM", $"RedisSessionModule registered", "Program");

//매니저 모듈 등록
ManagerModule.Register(builder.Services);
LogUtil.Info("SYSTEM", $"ManagerModule registered", "Program");

//공통 리소스 등록
//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources"); -> 리소스 폴더가 상위라서 적용하면 안댐
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization(options =>
{
	options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(SharedResource));
});

//EF 오류 표시
//if(serverType == "dev")
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
	LogUtil.Info("SYSTEM", $"Migrations endpoint enabled in Development environment.", "Program");
}
else
{
	app.UseExceptionHandler("/Error/Error");
	LogUtil.Info("SYSTEM", $"ExceptionHandler configured for non-Development environment.", "Program");
}
LogUtil.Info("SYSTEM", $"Static files middleware configured.", "Program");

//https
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
LogUtil.Info("SYSTEM", $"Routing configured.", "Program");

app.UseAuthorization();
LogUtil.Info("SYSTEM", $"Authorization enabled.", "Program");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Main}/{action=Main}/{id?}");
LogUtil.Info("SYSTEM", $"Default controller route configured.", "Program");

app.MapRazorPages();
LogUtil.Info("SYSTEM", $"Razor pages mapped.", "Program");

app.Run();