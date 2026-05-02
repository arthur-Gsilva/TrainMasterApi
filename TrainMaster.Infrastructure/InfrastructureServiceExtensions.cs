using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TrainMaster.Application.Interfaces;
using TrainMaster.Domain.Interfaces;
using TrainMaster.Infrastructure.Data;
using TrainMaster.Infrastructure.Services;

namespace TrainMaster.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ── Banco de dados ───────────────────────────────────────────────────
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // ── JWT ──────────────────────────────────────────────────────────────
        services.AddScoped<IJwtService, JwtService>();

        var secret = configuration["Jwt:Secret"]
            ?? throw new InvalidOperationException("Jwt:Secret is not configured.");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer           = true,
                ValidIssuer              = configuration["Jwt:Issuer"],
                ValidateAudience         = true,
                ValidAudience            = configuration["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateLifetime         = true,
                ClockSkew                = TimeSpan.Zero  // sem tolerância — expire é expire
            };
        });

        services.AddAuthorization();

        return services;
    }
}
