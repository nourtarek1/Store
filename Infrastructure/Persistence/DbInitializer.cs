using Domian.Contracts;
using Domian.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public async Task InitializeAsync()
        {
            // Create Database If It Dosent Esists  && Aply To Any Pending Migration 
            if (_context.Database.GetPendingMigrations().Any())
            {
                await _context.Database.MigrateAsync();
            }
            // Data Seeding


            //Seeding ProductTypes From Jeson Files 
            if (!_context.productTypes.Any())
            {

                // 1.Read All Data From Types json to String
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                // 2. TransForm To String To C# object [List<ProductType>]
                var types =    JsonSerializer.Deserialize<List<ProductType>>(typesData);

                // 3. Add To DataBase
                   if(types is not null && types.Any())
                {
                    await _context.productTypes.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                }
            }


            //Seeding ProductBrands From Jeson Files 
            if (!_context.productBrands.Any())
            {

                // 1.Read All Data From Types json to String
                var brandData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

                // 2. TransForm To String To C# object [List<ProductType>]
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                // 3. Add To DataBase
                if (brands is not null && brands.Any())
                {
                    await _context.productBrands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }
            }
            //Seeding Product From Jeson Files 
            if (!_context.products.Any())
            {

                // 1.Read All Data From Types json to String
                var ProductData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

                // 2. TransForm To String To C# object [List<ProductType>]
                var products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                // 3. Add To DataBase
                if (products is not null && products.Any())
                {
                    await _context.products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
