using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Need;
using ProfileService.Models.Common;

namespace ProfileService.Repositories.Interfaces
{
    public interface ILookupNeedRepository : IGenericRepository<Need>
    {
        Task<ICollection<Need>> SearchAsync(SearchLookupNeed request);
    }
}