using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Interest;

namespace ProfileService.Services.Interfaces
{
    public interface ILookupInterestService : IService
    {
        Task<ICollection<GetLookupInterest>> SearchAsync(SearchLookupInterest request);
        Task<GetLookupInterest> GetByIdAsync(Guid id);
        Task InsertAsync(NewLookupInterest interest);
        Task UpdateAsync(UpdateLookupInterest interest);
        Task DeleteAsync(Guid id);
    }
}