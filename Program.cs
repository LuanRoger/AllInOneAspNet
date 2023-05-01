using System.Text;
using AllInOneAspNet.Controllers;
using AllInOneAspNet.Endpoints;
using AllInOneAspNet.Models.ClientModels;
using AllInOneAspNet.Models.UserModels;
using AllInOneAspNet.Repositories;
using AllInOneAspNet.Repositories.Contexts;
using AllInOneAspNet.Services.Jwt;
using AllInOneAspNet.Validators.ClientValidators;
using AllInOneAspNet.Validators.UserValidators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
    const string connectionString = @"Data Source=AllInOneDatabase.db;";
    options.UseSqlite(connectionString);
});
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ClientRepository>();

builder.Services.AddScoped<IValidator<UserSigninRequestModel>, UserSigninRequestValidator>();
builder.Services.AddScoped<IValidator<UserLoginRequestModel>, UserLoginRequestValidator>();
builder.Services.AddScoped<IValidator<ClientRegisterRequestModel>, ClientRegisterRequestValidator>();
builder.Services.AddScoped<IValidator<ClientUpdateRequestModel>, ClientUpdateRequestValidator>();

builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<ClientController>();

builder.Services.AddSingleton<JwtService>(_ =>
{
    byte[] byteKey = Encoding.UTF8.GetBytes(JwtConsts.JWT_SIMETRIC_KEY_SHA256);
    
    return new(byteKey);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    byte[] byteKey = Encoding.UTF8.GetBytes(JwtConsts.JWT_SIMETRIC_KEY_SHA256);
    
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(byteKey),
        ValidIssuer = JwtConsts.JWT_ISSUER,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "AllInOneAspNet",
        Version = "v1",
        Description = "Web API example with ASP.NET Core"
    });
    options.AddSecurityDefinition("Bearer", new()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http, 
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new()
    {
        {
            new()
            {
                Reference = new()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    DatabaseContext dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dbContext.Database.EnsureCreated();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

RouteGroupBuilder userGroup = app.MapGroup("user")
    .AllowAnonymous();
userGroup.MapUserEndpoints();

RouteGroupBuilder clientGroup = app.MapGroup("client")
    .RequireAuthorization(policyBuilder => policyBuilder.RequireClaim(JwtConsts.CLAIM_ID));
clientGroup.MapClientEndpoints();

app.Run();
