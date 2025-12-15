using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelBooking.Application.Features.Auth;
using HotelBooking.Infrastructure.Authentication.Entities;
using HotelBooking.Infrastructure.Authentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HotelBooking.Infrastructure.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SECTION_NAME));

        var connectionString = configuration.GetConnectionString("AuthorizationDb")
                               ?? throw new InvalidOperationException("Connection string 'AuthorizationDb' not found.");

        services.AddDbContext<AuthorizationDbContext>(options =>
        {
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                mysql =>
                {
                    mysql.MigrationsAssembly(typeof(AuthorizationDbContext).Assembly.FullName);
                });
        });

        services
            .AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddEntityFrameworkStores<AuthorizationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<TokenService>();
        services.AddScoped<ITwoFactorService, TwoFactorService>();
        services.AddScoped<IAuthService, AuthService>();

        var jwt = configuration.GetSection(JwtSettings.SECTION_NAME).Get<JwtSettings>()
                  ?? throw new InvalidOperationException("JwtSettings not found");

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                    ClockSkew = TimeSpan.Zero,
                    NameClaimType = JwtRegisteredClaimNames.UniqueName,
                    RoleClaimType = ClaimTypes.Role
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context => Task.CompletedTask,
                    OnAuthenticationFailed = context => Task.CompletedTask
                };
            });

        services.AddAuthorization();

        return services;
    }
}