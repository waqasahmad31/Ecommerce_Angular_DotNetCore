using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext db;

        public ProductRepository(AppDbContext db)
        {
            this.db = db;
        }

        // *** Brand Code Here *** //


        public async Task<ProductType> GetProductTypeByIdAsync(int id)
        {
            return await db.ProductTypes.FindAsync(id);
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await db.ProductTypes.ToListAsync();
        }

        // *** Brand Code Here *** //


        public async Task<ProductBrand> GetProductBrandByIdAsync(int id)
        {
            return await db.ProductBrands.FindAsync(id);
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await db.ProductBrands.ToListAsync();
        }


        // *** Product Code Here *** //


        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await db.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p=>p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await db.Products
                .Include(p=>p.ProductType)
                .Include(p=>p.ProductBrand)
                .ToListAsync();
        }
    }
}
