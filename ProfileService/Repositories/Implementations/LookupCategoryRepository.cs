using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Lookup.Category;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class LookupCategoryRepository : GenericRepository<Category>, ILookupCategoryRepository
    {
        private readonly ProfileServiceContext _context;
        public LookupCategoryRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Category>> SearchAsync(SearchLookupCategory request)
        {
            IQueryable<Category> query = _context.LookupCategories;
            
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(q => q.Name.ToLower().Contains(request.Name.ToLower()));
            }

            return await query.ToListAsync();
        }
    }
}