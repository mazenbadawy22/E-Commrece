using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.Basket;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
    {
        private readonly IDatabase _database=connectionMultiplexer.GetDatabase();
        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
           var Value = await _database.StringGetAsync(id);
            if(Value.IsNullOrEmpty)
                return null;

            return JsonSerializer.Deserialize<CustomerBasket>(Value);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeSpan = null)
        {
            var JsonBasket =  JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket,timeSpan?? TimeSpan.FromDays(30));
            return IsCreatedOrUpdated? await GetBasketAsync(basket.Id) : null;
        }
        public async Task<bool> DeleteBasketAsync(string id)
       =>await _database.KeyDeleteAsync(id);
    }
}
