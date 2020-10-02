using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Models.Common;

namespace ProfileService.Repositories.Interfaces
{
    public interface ILookupInterestRepository : IGenericRepository<Interest>
    {
        Task<ICollection<Interest>> SearchAsync(SearchLookupInterest request);
    }
}