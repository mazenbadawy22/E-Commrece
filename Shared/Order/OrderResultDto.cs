using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Order
{
    public record OrderResultDto
    {
        public Guid Id { get; init; }
        public string UserEmail { get; init; }

        public ShippingAddressDto ShippingAddress { get; init; }
        public ICollection<OrderItemDto> OrderItems { get; init; }=new List<OrderItemDto>(); 
        public string DeliveryMethod { get; init; }
        public string PaymentStatus { get; init; }

        public decimal SubTotal { get; init; }
        public DateTimeOffset OrderDate { get; init; }=DateTimeOffset.Now;
        public decimal Total {  get; init; }
    }
}
