using Core.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Specifications;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T:BaseEntity
    {
        private readonly AppDbContext db;

        public GenericRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<int> CountAsynx(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(db.Set<T>().AsQueryable(), specification);
        }
    }
}
