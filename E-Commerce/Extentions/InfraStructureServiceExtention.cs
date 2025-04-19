using Domain.Contracts;
using Persistence.Repositories;
using Persistence;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shared.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.Extentions
{
    public static class InfraStructureServiceExtention
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection Services , IConfiguration configuration)
        {
            Services.AddScoped<IDbInitializer, DbInitializer>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddDbContext<StoreContext>((options) =>
            {
                options.UseSqlServer( configuration.GetConnectionString("DefaultConnection"));
            });
            Services.AddDbContext<StoreIdentityContext>((options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            Services.AddSingleton<IConnectionMultiplexer>(
              _ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));
            Services.ConfigrationIdentityService();
            Services.ConfigureJwt(configuration);
			//return
			return Services;
        }
        public static IServiceCollection ConfigrationIdentityService(this IServiceCollection Services)
        {
            Services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<StoreIdentityContext>();
            return Services;
        }    
        public static IServiceCollection ConfigureJwt(this IServiceCollection Services, IConfiguration configuration)
        {
            var jwtoptions = configuration.GetSection("JWtOptions").Get<Jwtoptions>();
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtoptions.Issure,
                ValidAudience = jwtoptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.SecretKey))
			});
            Services.AddAuthorization();
            return Services;
        }
    }
}
