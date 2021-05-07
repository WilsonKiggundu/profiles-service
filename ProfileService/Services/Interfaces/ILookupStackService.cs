using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Models.Common;

namespace ProfileService.Services.Interfaces
{
    public interface ILookupStackService : IService
    {
        Task<ICollection<TechStack>> SearchAsync(string name);
        Task<TechStack> GetByIdAsync(Guid id);
        Task InsertAsync(TechStack stack);
        Task InsertManyAsync(ICollection<TechStack> stacks);
        Task UpdateAsync(TechStack stack);
        Task DeleteAsync(Guid id);
    }
}