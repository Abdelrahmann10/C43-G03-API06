using Services.Abstractions;
using Services;
using E_Commerce.Factoris;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Extentions
{
    public static class  PeresentationServicesExtensions
    {
        public static IServiceCollection AddPersentationServices(this IServiceCollection Services)
        {
            Services.AddControllers().AddApplicationPart(typeof(Persentation.AssemblyReference).Assembly);
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrors;

            });
            return Services;
        }
    }
}
