using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            #region Product
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,3)");
            #endregion
            #region ProductBrand
            builder.HasOne(product => product.productBrand)
                .WithMany()
                .HasForeignKey(product => product.BrandId);
            builder.HasOne(product => product.productType)
                .WithMany()
                .HasForeignKey(product => product.TypeId);
                
            #endregion
            #region ProductType

            #endregion
        }
    }
}
