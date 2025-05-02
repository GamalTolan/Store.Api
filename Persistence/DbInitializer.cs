using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.Identity.OrderEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;
        private readonly StoreIdentityDbContext _identityDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public DbInitializer(StoreDbContext context ,StoreIdentityDbContext identityDbContext ,RoleManager<IdentityRole> roleManager,UserManager<User> userManager)
        {
            _context = context;
            _identityDbContext = identityDbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task Initialize()
        {
            //if(_context.Database.GetPendingMigrations().Any())
            //{
            //    _context.Database.Migrate();
            //}

            if (!_context.ProductTypes.Any())
            {
                var typeData=File.ReadAllText(@"..\Persistence\Data\Seeding\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

                if (types is not null && types.Any())
                { _context.ProductTypes.AddRange(types);
                    _context.SaveChanges();
                }
            }

            if (!_context.ProductBrands.Any())
            {
                var brandData = File.ReadAllText(@"..\Persistence\Data\Seeding\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if (brands is not null && brands.Any())
                {
                    _context.ProductBrands.AddRange(brands);
                    _context.SaveChanges();
                }
            }
            if (!_context.Products.Any())
            {
                var productData = File.ReadAllText(@"..\Persistence\Data\Seeding\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (products is not null && products.Any())
                {
                    _context.Products.AddRange(products);
                    _context.SaveChanges();
                }
            }
            if (!_context.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText(@"..\Persistence\Data\Seeding\delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                if (deliveryMethods is not null && deliveryMethods.Any())
                {
                    _context.DeliveryMethods.AddRange(deliveryMethods);
                    _context.SaveChanges();
                }
            }
        }

        public async Task InitializeIdentityAsync()
        {

            if (_identityDbContext.Database.GetPendingMigrations().Any())
            {
               await _identityDbContext.Database.MigrateAsync();
            }

            if(!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if(!_userManager.Users.Any())
            {
                var superAdminUser = new User
                {
                    DisplayName = "SuperAdmin",
                    Email="SuperAdmin4@gmail.com",
                    UserName="Super Admin",
                    PhoneNumber= "123456789",


                };

                var adminUser = new User
                {
                    DisplayName = "Admin",
                    Email = "Admin45@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "12345678910",


                };

                await _userManager.CreateAsync(superAdminUser, "Pa$$w0rd");
                await _userManager.CreateAsync(adminUser, "Pa$$w0rd");
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
