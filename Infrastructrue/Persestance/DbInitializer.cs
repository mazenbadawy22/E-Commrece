using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _storeContext;

        public DbInitializer(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task InitializeAsync()
        {
            try
            {
                if (_storeContext.Database.GetPendingMigrations().Any())
                    await _storeContext.Database.MigrateAsync();
                if (!_storeContext.ProductTypes.Any())
                {
                    var TypesData = await File.ReadAllTextAsync("..\\Infrastructrue\\Persestance\\Data\\Seeding\\types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                    if (Types is not null && Types.Any())
                    {
                        await _storeContext.ProductTypes.AddRangeAsync(Types);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                if (!_storeContext.ProductBrands.Any())
                {
                    var BrandsData = await File.ReadAllTextAsync("..\\Infrastructrue\\Persestance\\Data\\Seeding\\brands.json");
                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                    if (Brands is not null && Brands.Any())
                    {
                        await _storeContext.ProductBrands.AddRangeAsync(Brands);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                if (!_storeContext.Products.Any())
                {
                    var ProductData = await File.ReadAllTextAsync("..\\Infrastructrue\\Persestance\\Data\\Seeding\\products.json");
                    var Product = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    if (Product is not null && Product.Any())
                    {
                        await _storeContext.Products.AddRangeAsync(Product);
                        await _storeContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
