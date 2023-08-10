using Core.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class SeedDB
    {
        public static async Task Initialize(AppDbContext appDbContext, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!appDbContext.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    foreach (var brand in brands)
                    {
                        appDbContext.ProductBrands.Add(brand);
                    }
                    await appDbContext.SaveChangesAsync();
                }
                if (!appDbContext.ProductTypes.Any())
                {
                    var productTypeData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypeData);

                    foreach (var productType in productTypes)
                    {
                        appDbContext.ProductTypes.Add(productType);
                    }
                    await appDbContext.SaveChangesAsync();
                }
                if (!appDbContext.Products.Any())
                {
                    var productData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    foreach (var product in products)
                    {
                        appDbContext.Products.Add(product);
                    }
                    await appDbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AppDbContext>();
                logger.LogError(ex.Message);
            }

            //var context = serviceProvider.GetRequiredService<AppDbContext>();
            //context.Database.EnsureCreated();
            //if (!context.Products.Any())
            //{
            //    ProductType type = new ProductType()
            //    {
            //        Name = "Type 1"
            //    };
            //    context.ProductTypes.Add(type);

            //    ProductBrand brand = new ProductBrand()
            //    {
            //        Name = "DELL"
            //    };
            //    context.ProductBrands.Add(brand);

            //    Product product = new Product()
            //    {
            //        Name = "Product 1",
            //        Description = "Build a proof of concept e-commerce store using Angular, .Net and Stripe for payment processing",
            //        PictureUrl = string.Empty,  
            //        Price = 1500,
            //        ProductBrandId = 1,
            //        ProductTypeId = 1,
            //    };
            //    context.Products.Add(product);

            //    context.SaveChanges();
            //}
        }
    }
}
