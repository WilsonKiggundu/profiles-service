using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Need;

namespace ProfileService.Services.Interfaces
{
    public interface ILookupNeedService : IService
    {
        Task<ICollection<GetLookupNeed>> SearchAsync(SearchLookupNeed request);
        Task<GetLookupNeed> GetByIdAsync(Guid id);
        Task InsertAsync(NewLookupNeed category);
        Task UpdateAsync(UpdateLookupNeed category);
        Task DeleteAsync(Guid id);
    }
}