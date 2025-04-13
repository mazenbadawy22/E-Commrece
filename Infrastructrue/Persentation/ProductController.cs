using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Persentation
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductController(IServiceManager _serviceManager) :ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Paginated<ProductResultDto>>> GetAllProducts([FromQuery]ProductParameterSpceifications parameters)
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync(parameters);
            return Ok(Products);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDto>> GetProduct(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }


    }
}
