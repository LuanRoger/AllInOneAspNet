using AllInOneAspNet.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

RouteGroupBuilder userGroup = app.MapGroup("user");
userGroup.MapUserEndpoints();

RouteGroupBuilder clientGroup = app.MapGroup("client");
clientGroup.MapClientEndpoints();

app.Run();
