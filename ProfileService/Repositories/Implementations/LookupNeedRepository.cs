using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Need;
using ProfileService.Data;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class LookupNeedRepository : GenericRepository<Need>, ILookupNeedRepository
    {
        public LookupNeedRepository(ProfileServiceContext context) : base(context) { }
        public async Task<ICollection<Need>> SearchAsync(SearchLookupNeed request)
        {
            throw new System.NotImplementedException();
        }
    }
}