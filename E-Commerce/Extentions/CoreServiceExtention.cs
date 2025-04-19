using Services.Abstractions;
using Services;
using Shared.Security;

namespace E_Commerce.Extentions
{
    public static class   CoreServiceExtention
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            Services.Configure<Jwtoptions>(configuration.GetSection("JWtOptions"));
            return Services;
        }
    }
}
