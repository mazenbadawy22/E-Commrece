using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.Identity;
using Domain.Entities.OrderEntites;
using Shared.Order;
using Shared.Security;

namespace Services.MappingProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            #region ShippingAddress
            CreateMap<ShippingAddress,ShippingAddressDto>().ReverseMap();
            #endregion
            #region OrderItem
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(o => o.ProductId, p => p.MapFrom(p => p.Product.ProductID))
                .ForMember(o => o.ProductName, p => p.MapFrom(p => p.Product.ProductName))
                .ForMember(o => o.PictureUrl, p => p.MapFrom(p => p.Product.PictureUrl)).ReverseMap();
            #endregion
            #region Order
            CreateMap<Order,OrderResultDto>()
                .ForMember(o => o.PaymentStatus, p => p.MapFrom(p => p.ToString()))
                .ForMember(o => o.DeliveryMethod, p => p.MapFrom(p => p.DeliveryMethod.ShortName))
                .ForMember(o => o.Total, p => p.MapFrom(p => p.SubTotal+p.DeliveryMethod.Price)).ReverseMap();
            #endregion
            #region DeliveryMethod
            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
            #endregion
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
