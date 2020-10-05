using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Data;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class LookupInterestRepository : GenericRepository<Interest>, ILookupInterestRepository
    {
        private readonly ProfileServiceContext _context;
        public LookupInterestRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ICollection<Interest>> SearchAsync(SearchLookupInterest request)
        {
            IQueryable<Interest> query = _context.LookupInterests;

            if (!string.IsNullOrEmpty(request.Category))
            {
                query = query.Where(q => q.Category.ToLower().Contains(request.Category.ToLower()));
            }

            return await query.ToListAsync();
        }
    }
}