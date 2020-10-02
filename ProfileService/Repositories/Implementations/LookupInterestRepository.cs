using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Data;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class LookupInterestRepository : GenericRepository<Interest>, ILookupInterestRepository
    {
        public LookupInterestRepository(ProfileServiceContext context) : base(context) { }
        public async Task<ICollection<Interest>> SearchAsync(SearchLookupInterest request)
        {
            throw new System.NotImplementedException();
        }
    }
}