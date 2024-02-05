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

/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true; // https : true
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
        ClockSkew = TimeSpan.Zero
    };
});*/

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
