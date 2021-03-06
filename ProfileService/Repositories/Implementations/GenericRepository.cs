using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly ProfileServiceContext _context;
        private readonly DbSet<T> _entities;

        protected GenericRepository(ProfileServiceContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task InsertManyAsync(ICollection<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            await _entities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity)); 
            
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var entity = await _entities.SingleOrDefaultAsync(s => s.Id == id);

            entity.IsDeleted = true;

            await UpdateAsync(entity);
        }
    }
}