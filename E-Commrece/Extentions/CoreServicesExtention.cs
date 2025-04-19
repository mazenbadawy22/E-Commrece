using Services.Abstractions;
using Services;
using Shared.Security;

namespace E_Commrece.Extentions
{
    public static class CoreServicesExtention
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(typeof(Services.AssmblyRefrence).Assembly);
            Services.Configure<JwtOptions>(configuration.GetSection("JWtOptions"));
            return Services;
        }
    }
}
