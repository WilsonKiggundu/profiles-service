using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.Models;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly ProfileServiceContext _context;
        private readonly DbSet<T> _entities;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(ProfileServiceContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task InsertAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteAsync(Guid id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            T entity = await _entities.SingleOrDefaultAsync(s => s.Id == id);
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}