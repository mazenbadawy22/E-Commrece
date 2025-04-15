using Domain.Contracts;
using Persistence.Repositories;
using Persistence;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace E_Commrece.Extentions
{
    public static class InfrastructureServiceExtention
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddScoped<IDbInitializer, DbInitializer>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();

            Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            Services.AddSingleton<IConnectionMultiplexer>(
                _=> ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));

            return Services;
        }
    }
}
