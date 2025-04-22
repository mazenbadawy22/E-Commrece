using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Order;

namespace Services.Abstractions
{
    public interface IOrderServices
    {
         public Task<OrderResultDto> GetOrderByIdAsync(Guid id);
        public Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string Email);
        public Task<OrderResultDto> CreateOrderAsync(OrderRequest request, string UserEmail);
        public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync(); 
    }
}
