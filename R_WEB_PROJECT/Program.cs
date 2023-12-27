using Microsoft.EntityFrameworkCore;
using log4net.Config;
using log4net;
using System.Reflection;
using R_WEB_PROJECT.Modules.ServiceModule;
using R_WEB_PROJECT.Modules.Session;
using R_WEB_PROJECT.RedisStore.Session;
using Microsoft.Extensions.DependencyInjection;
using R_WEB_PROJECT.Utilities.Log;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var serverType = configuration.GetValue<string>("ServerType");
GlobalContext.Properties["ServerType"] = serverType;
Log.Info("SYSTEM", $"Server Type: {serverType}");

//로그
//Add log4net as logging provider
//logger.Info, Debug, Warn, Error, Fatal
//{0} : 첫번째 매개변수,$ 포함한 {count} 변수 count
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo(serverType == "real" ? "log4net_real.config" : "log4net_dev.config"));
var _logger = LogManager.GetLogger(typeof(Program));
Log.Info("SYSTEM", "Logger registered");

//의존성 주입
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//DB
var connectionString = configuration.GetConnectionString(serverType == "real" ? "RealConnection" : "DevConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
Log.Info("SYSTEM", $"Database connection string: {connectionString}");
builder.Services.AddScoped(provider => new DatabaseManager(connectionString));

//레디스 셋팅
var redisConnectionString = builder.Configuration.GetConnectionString(serverType == "real" ? "RealRedisConnection" : "DevRedisConnection");
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = redisConnectionString;
	options.InstanceName = "RWRedisInstance"; // 선택적으로 인스턴스 이름 설정
});

//서비스 모듈 등록
ServiceModule.Register(builder.Services);
Log.Info("SYSTEM", $"ServiceModule registered");

//레포지토리 모듈 등록
RepositoryModule.Register(builder.Services);
Log.Info("SYSTEM", $"RepositoryModule registered");

//레디스 세션 모듈 등록
RedisSessionModule.Register(builder.Services);
Log.Info("SYSTEM", $"RedisSessionModule registered");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
	Log.Info("SYSTEM", $"Migrations endpoint enabled in Development environment.");
}
else
{
	app.UseExceptionHandler("/Error/Error");
	Log.Info("SYSTEM", $"ExceptionHandler configured for non-Development environment.");
}
Log.Info("SYSTEM", $"Static files middleware configured.");

app.UseStaticFiles();

app.UseRouting();
Log.Info("SYSTEM", $"Routing configured.");

app.UseAuthorization();
Log.Info("SYSTEM", $"Authorization enabled.");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Main}/{action=Main}/{id?}");
Log.Info("SYSTEM", $"Default controller route configured.");

app.MapRazorPages();
Log.Info("SYSTEM", $"Razor pages mapped.");

app.Run();