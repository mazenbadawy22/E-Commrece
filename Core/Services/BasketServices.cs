using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Basket;
using Domain.Exceptions;
using Services.Abstractions;
using Shared;

namespace Services
{
    public class BasketServices(IBasketRepository basketRepository,IMapper Mapper) : IBasketServices
    {
        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var Basket =  await basketRepository.GetBasketAsync(id);
            return Basket is null ? throw new BasketNotFoundExcption(id)
                : Mapper.Map<BasketDto>(Basket);
           
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = Mapper.Map<CustomerBasket>(basket);
            var UpdatedBasket=await basketRepository.UpdateBasketAsync(CustomerBasket);
            return UpdatedBasket is null ? throw new Exception("We Can Not Update Basket")
                : Mapper.Map<BasketDto>(UpdatedBasket);
        }
        public async Task<bool> DeleteBasketAsync(string id)
       => await basketRepository.DeleteBasketAsync(id);
    }
}
