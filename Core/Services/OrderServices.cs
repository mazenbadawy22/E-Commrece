using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Basket;
using Domain.Entities.OrderEntites;
using Domain.Exceptions;
using Domain.Exceptions.Product;
using Services.Abstractions;
using Services.Specifications;
using Shared.Order;

namespace Services
{
    public class OrderServices(IMapper mapper,IBasketRepository basketRepository,IUnitOfWork unitOfWork) : IOrderServices
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequest request, string UserEmail)
        {
           var ShippingAddress = mapper.Map<ShippingAddress>(request.ShippingAddress);
            var Basket = await basketRepository.GetBasketAsync(request.BasketId)
                ?? throw new BasketNotFoundExcption(request.BasketId);
            var OrderItems = new List<OrderItem>();
            foreach(var item in Basket.items)
            {
                var Product = await unitOfWork.GetRepository<Product,int>()
                    .GetByIdAsync(item.Id)??throw new ProductNotFoundExeption(item.Id);
                OrderItems.Add(CreateOrderItem(item, Product));
                
            }
            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
                    .GetByIdAsync(request.DeliveryMethodId) ??
                    throw new DeliveryMethodExcptions(request.DeliveryMethodId);
            var SubTotal = OrderItems.Sum(d => d.Price * d.Quantity);
            var Order = new Order(UserEmail, ShippingAddress, OrderItems, DeliveryMethod, SubTotal);
            await unitOfWork.GetRepository<Order, Guid>()
                .AddAsync(Order);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<OrderResultDto>(Order);
        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
        => new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl),
            item.Quantity, item.Price);


        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod,int>()
                .GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodDto>>(DeliveryMethod);
        }

        public async  Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            var Order = unitOfWork.GetRepository<Order, Guid>()
                  .GetByIdAsync(new OrderWithIncludeSpecefications(id))
                  ?? throw new OrderNotFoundExcptions(id);
            return mapper.Map<OrderResultDto>(Order); 
        }

        public async Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string Email)
        {
            var Orders = unitOfWork.GetRepository<Order, Guid>()
                .GetAllAsync(new OrderWithIncludeSpecefications(Email));
            return mapper.Map<IEnumerable<OrderResultDto>>(Orders);

        }
    }
}
