using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
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
        public DbInitializer(StoreDbContext context)
        {
            _context = context;

        }
        public void Initialize()
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
        }
    }
}
