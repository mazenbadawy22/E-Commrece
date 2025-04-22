using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntites
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {

        }
        public Order (string _UserEmail,ShippingAddress _ShippingAddress,ICollection<OrderItem> _OrderItems,DeliveryMethod _DeliveryMethod,decimal _Subtotal)
        {
            Id = Guid.NewGuid();
            UserEmail = _UserEmail;
            ShippingAddress = _ShippingAddress;
            orderItems = _OrderItems;
            DeliveryMethod = _DeliveryMethod;
            SubTotal = _Subtotal;
        }
        public string UserEmail { get; set; }
        public ShippingAddress  ShippingAddress {  get; set; }  
        public ICollection<OrderItem> orderItems { get; set; }
        public OrderPaymentStatus PaymentStatus { get; set; }
        public int? DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
    }
}
