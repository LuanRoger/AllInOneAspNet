using AllInOneAspNet.Endpoints;
using AllInOneAspNet.Models.ClientModels;
using AllInOneAspNet.Models.UserModels;
using AllInOneAspNet.Repositories;
using AllInOneAspNet.Repositories.Contexts;
using AllInOneAspNet.Validators.ClientValidators;
using AllInOneAspNet.Validators.UserValidators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ILogger = Serilog.ILogger;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Host.UseSerilog(logger);

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    string connectionString = @"Data Source=AllInOneDatabase.db;Version=3;";
    options.UseSqlite(connectionString);
});
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ClientRepository>();

builder.Services.AddScoped<IValidator<UserSigninRequestModel>, UserSigninRequestValidator>();
builder.Services.AddScoped<IValidator<UserLoginRequestModel>, UserLoginRequestValidator>();
builder.Services.AddScoped<IValidator<ClientRegisterRequestModel>, ClientRegisterRequestValidator>();
builder.Services.AddScoped<IValidator<ClientUpdateRequestModel>, ClientUpdateRequestValidator>();

WebApplication app = builder.Build();

RouteGroupBuilder userGroup = app.MapGroup("user");
userGroup.MapUserEndpoints();

RouteGroupBuilder clientGroup = app.MapGroup("client");
clientGroup.MapClientEndpoints();

app.Run();
