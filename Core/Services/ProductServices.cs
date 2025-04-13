using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Services.Specifications;
using Shared;

namespace Services
{
    public class ProductServices(IUnitOfWork unitOfWork,IMapper Mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var Brands = await unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            var BrandsResult = Mapper.Map<IEnumerable<BrandResultDto>>(Brands);
            return BrandsResult;
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int Id)
        {
            var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(new ProductWithBrandAndTypeSpcefications(Id));
            var ProductResult = Mapper.Map<ProductResultDto>(Product);
            return ProductResult;
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(string? sort, int? brandId, int? typeId)
        {
            var Products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpcefications(sort,brandId,typeId));
            var ProductsResult = Mapper.Map<IEnumerable<ProductResultDto>>(Products);
            return ProductsResult;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesResult = Mapper.Map<IEnumerable<TypeResultDto>>(Types);
            return TypesResult;
        }
    }
}
