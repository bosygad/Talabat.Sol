using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Repository.Data.BDContext;

namespace Talabat.Repository.Data
{
    public class StoreContextSeed
    {
       
        public static async Task SeedAsync(StoreDbContext dbContext)
        {
            if (!dbContext.ProductBrands.Any())
            {
                //Seeding Brands
                var BrandsData = File.ReadAllText("../Talabat.Repository/Data/Data Seed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        await dbContext.Set<ProductBrand>().AddAsync(Brand);

                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.ProductTypes.Any())
            {
                // Seeding Types
                var TypesData = File.ReadAllText("../Talabat.Repository/Data/Data Seed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await dbContext.Set<ProductType>().AddAsync(Type);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
            if (!dbContext.products.Any())
            {
                //Seeding Products
                var ProductsData = File.ReadAllText("../Talabat.Repository/Data/Data Seed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products?.Count > 0)
                {
                    foreach (var Product in Products)
                    {
                        await dbContext.Set<Product>().AddAsync(Product);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }

      
    }
}
