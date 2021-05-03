#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class LookupStackRepository : GenericRepository<TechStack>, ILookupStackRepository
    {
        private readonly ProfileServiceContext _context;
        public LookupStackRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ICollection<TechStack>> SearchAsync(string name)    
        {
            IQueryable<TechStack> query = _context.LookupStacks;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(q => q.Name.ToLower().Contains(name.ToLower()));
            }
            
            return await query.ToListAsync();
        }
    }
}