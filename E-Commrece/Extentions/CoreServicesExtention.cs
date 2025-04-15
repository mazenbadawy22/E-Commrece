using Services.Abstractions;
using Services;

namespace E_Commrece.Extentions
{
    public static class CoreServicesExtention
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(typeof(Services.AssmblyRefrence).Assembly);
            return Services;
        }
    }
}
