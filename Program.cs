using AllInOneAspNet.Endpoints;
using Serilog;
using ILogger = Serilog.ILogger;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Host.UseSerilog(logger);
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ClientRepository>();
WebApplication app = builder.Build();

RouteGroupBuilder userGroup = app.MapGroup("user");
userGroup.MapUserEndpoints();

RouteGroupBuilder clientGroup = app.MapGroup("client");
clientGroup.MapClientEndpoints();

app.Run();
