using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Models.Common;

namespace ProfileService.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(Guid id);
        Task InsertAsync(T entity);
        Task InsertManyAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}