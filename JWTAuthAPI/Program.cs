using log4net.Config;
using log4net;
using JWTAuthAPI.Utilities.Log;
using System.Reflection;
using JWTAuthAPI.Modules.ServiceModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

// Add services to the container.
//서비스 모듈 등록
ServiceModule.Register(builder.Services);
LogUtil.Info("SYSTEM", $"ServiceModule registered", "Program");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration[configuration["Jwt:Issuer"]],
        ValidAudience = builder.Configuration[configuration["Jwt:Audience"]],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
    };
});
builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseAuthentication();

app.Run();
