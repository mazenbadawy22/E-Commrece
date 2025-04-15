using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.Basket;
using Shared;

namespace Services.MappingProfiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket,BasketDto>()
                .ReverseMap();
            CreateMap<BasketItem,BasketIemDto>().ReverseMap();
        }
    }
}
