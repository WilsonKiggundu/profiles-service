using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Lookup.Need;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class LookupNeedRepository : GenericRepository<Need>, ILookupNeedRepository
    {
        private readonly ProfileServiceContext _context;
        public LookupNeedRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ICollection<Need>> SearchAsync(SearchLookupNeed request)
        {
            IQueryable<Need> needs = _context.LookupNeeds;

            if (!string.IsNullOrEmpty(request.Category))
            {
                needs = needs.Where(q => q.Category.ToLower().Contains(request.Category.ToLower()));
            }

            return await needs.ToListAsync();
        }
    }
}