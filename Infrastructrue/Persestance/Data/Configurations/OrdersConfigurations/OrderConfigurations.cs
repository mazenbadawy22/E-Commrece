using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackExchange.Redis;

namespace Persistence.Data.Configurations.Order
{
    public class OrderConfigurations : IEntityTypeConfiguration<Domain.Entities.OrderEntites.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.OrderEntites.Order> builder)
        {
            #region ShippingAddress
            builder.OwnsOne(p => p.ShippingAddress, p => p.WithOwner());
            #endregion
            #region OrederItems
            builder.HasMany(o => o.orderItems)
                .WithOne();
            #endregion
            #region Payment
            builder.Property(p => p.PaymentStatus)
                .HasConversion(s => s.ToString()
               , s => Enum.Parse <OrderPaymentStatus>(s));
            #endregion
            #region DeliverMethod
            builder.HasOne(p=>p.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            #endregion
            #region SubTotal
            builder.Property(p => p.SubTotal)
                .HasColumnType("decimal(18,3)");
            #endregion
        }
    }
}
