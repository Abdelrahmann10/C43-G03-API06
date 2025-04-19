using Domain.Contracts;
using E_Commerce.MiddleWareas;

namespace E_Commerce.Extentions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDbasync(this WebApplication app)
        {
            //Create Object From Type That Implements IDbInitializer
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializerAsync();
            await dbInitializer.InitializerIdentityAsync();
            return app;
        }
        public static WebApplication UsCustomMiddleWareExceptions(this WebApplication app)
        {
            app.UseMiddleware<GlobalExeptionHandlingMiddlewar>();
            return app;
        }
        
    }
}
