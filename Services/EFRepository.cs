using Microsoft.EntityFrameworkCore;

using SampelToDo.Models;
using SampelToDo.Services.Interfaces;

namespace SampelToDo.Services
{
    public class EFRepository<T> : IRepository<T> where T : BaseModel
    {
        private readonly DbContext _context;
        public EFRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync<T>(x => x.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            var oldEntity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (oldEntity != null)
            {
                oldEntity = entity;
                _context.Set<T>().Update(oldEntity);
                await _context.SaveChangesAsync();
                
            }

        }
        public async Task DeleteAsync(Guid id)
        {
           var entity = await _context.Set<T>().FirstOrDefaultAsync(x=>x.Id==id);
            if(entity is not null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
           await _context.SaveChangesAsync();
        }
    }
}
