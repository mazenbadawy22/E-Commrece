using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Shared;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpcefications:Specfications<Product>
    {
        public ProductWithBrandAndTypeSpcefications(int id) : base (product => product.Id == id)
        {
            AddInclude(product => product.productBrand);
            AddInclude(product => product.productType);
        }
        public ProductWithBrandAndTypeSpcefications(ProductParameterSpceifications parameters)
            :base (product=>
            (!parameters.BrandId.HasValue || product.BrandId== parameters.BrandId.Value ) && 
            (!parameters.TypeId.HasValue || product.TypeId== parameters.TypeId.Value) &&
            (string.IsNullOrWhiteSpace(parameters.Search)||product.Name.ToLower().Contains(parameters.Search.ToLower().Trim())))
        {
            AddInclude(product => product.productBrand);
            AddInclude(product => product.productType);
            ApplyPagination(parameters.PageIndex, parameters.PageSize);
            
            #region Sort
            if (parameters.Sort is not null)
            {
                switch (parameters.Sort)
                {
                    case ProductSortOptions.PriceDesc:
                        SetOrderByDescinding(p => p.Price);
                        break;
                    case ProductSortOptions.PriceAsc:
                        SetOrderBy(p => p.Price);
                        break;
                    case ProductSortOptions.NameDesc:
                        SetOrderByDescinding(p => p.Name);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }
            } 
            #endregion

        }
    }
}
