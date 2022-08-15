using Microsoft.EntityFrameworkCore;
using Report.Application.Interfaces.Repositories;
using Report.Persistence.Context;

namespace Report.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        private DbSet<T> Table { get => _context.Set<T>(); }

        public virtual async Task<T> Add(T entity)
        {
            T result = Table.AddAsync(entity).Result.Entity;
            var retVal = await _context.SaveChangesAsync();
            return result;
        }

        public virtual async Task<T> Update(T entity)
        {
            T result = Table.Update(entity).Entity;
            await _context.SaveChangesAsync();
            return result;
        }

        public virtual async Task<T> Delete(T entity)
        {
            T result = Table.Remove(entity).Entity;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<List<T>> GetAsync() => await Table.ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await Table.FindAsync(id);

        public async Task<int> SaveChanges()
        { 
            return await _context.SaveChangesAsync(); 
        }
    }
}
