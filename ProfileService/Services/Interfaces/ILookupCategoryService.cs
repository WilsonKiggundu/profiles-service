using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Category;

namespace ProfileService.Services.Interfaces
{
    public interface ILookupCategoryService : IService
    {
        Task<ICollection<GetLookupCategory>> SearchAsync(SearchLookupCategory request);
        Task<GetLookupCategory> GetByIdAsync(Guid id);
        Task InsertAsync(NewLookupCategory category);
        Task UpdateAsync(UpdateLookupCategory category);
        Task DeleteAsync(Guid id);
    }
}