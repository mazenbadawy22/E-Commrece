using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpcefications:Specfications<Product>
    {
        public ProductWithBrandAndTypeSpcefications(int id) : base (product => product.Id == id)
        {
            AddInclude(product => product.productBrand);
            AddInclude(product => product.productType);
        }
        public ProductWithBrandAndTypeSpcefications(string? sort , int? brandId , int? typeId)
            :base (product=>
            (!brandId.HasValue || product.BrandId==brandId.Value ) && 
        (!typeId.HasValue || product.TypeId==typeId.Value))
        {
            AddInclude(product => product.productBrand);
            AddInclude(product => product.productType);
            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort.ToLower().Trim())
                {
                    case "pricedesc":
                        SetOrderByDescinding(p => p.Price);
                        break;
                    case "priceasc":
                        SetOrderBy(p=>p.Price);
                        break;
                    case "namedesc":
                        SetOrderByDescinding(p => p.Name);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                } 
            }
        }
    }
}
