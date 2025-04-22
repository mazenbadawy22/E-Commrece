using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.OrderEntites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _storeContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(StoreContext storeContext,UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            _storeContext = storeContext;
            _userManager = userManager;
            _roleManager = roleManager;
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
                if (!_storeContext.DeliveryMethods.Any())
                {
                    var DeliveyData = await File.ReadAllTextAsync("..\\Infrastructrue\\Persestance\\Data\\Seeding\\delivery (1).json");
                    var Data = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveyData);
                    if (Data is not null && Data.Any()) 
                    {
                        await _storeContext.DeliveryMethods.AddRangeAsync(Data);
                        await _storeContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public async Task InitializeIdentityAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!_userManager.Users.Any())
            {
                var SuperAdminUser = new User
                {
                    DisplayName = "SuperAdminUser",
                    Email = "SuperAdminUser@gmail.com",
                    UserName = "SuperAdminUser",
                    PhoneNumber= "1234567890",
                };
                var AdminUser = new User
                {
                    DisplayName = "AdminUser",
                    Email = "AdminUser@gmail.com",
                    UserName = "AdminUser",
                    PhoneNumber = "1234567890",
                };
                await _userManager.CreateAsync(SuperAdminUser,"Passw0rd");
                await _userManager.CreateAsync(AdminUser,"Passw0rd");
                await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(AdminUser, "Admin");
            }
        }
    }
}
