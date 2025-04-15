using Domain.Contracts;
using E_Commrece.Middlewares;

namespace E_Commrece.Extentions
{
    public static class WebApplicationExtention
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbintializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbintializer.InitializeAsync();
            return app;
        }
        public static WebApplication UseCustomMiddleWareExceptions(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleWare>();
            return app;
        }
    }
}
