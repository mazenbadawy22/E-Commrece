using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions.Product;
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
            return Product is null? throw new ProductNotFoundExeption(Id) : Mapper.Map<ProductResultDto>(Product);
           
        }

        public async Task<Paginated<ProductResultDto>> GetAllProductsAsync(ProductParameterSpceifications parameters)
        {
            var Products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpcefications(parameters));
            var ProductsResult = Mapper.Map<IEnumerable<ProductResultDto>>(Products);
            var Count = ProductsResult.Count();
            var totalCount = await unitOfWork.GetRepository<Product, int>()
                .CountAsync(new ProductCountSpceifications(parameters));
            var Result = new Paginated<ProductResultDto>(
                parameters.PageIndex,
                parameters.PageSize,
                totalCount,
                ProductsResult
                );
            return Result;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesResult = Mapper.Map<IEnumerable<TypeResultDto>>(Types);
            return TypesResult;
        }
    }
}
