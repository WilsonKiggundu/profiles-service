#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class LookupSchoolRepository : GenericRepository<School>, ILookupSchoolRepository
    {
        private readonly ProfileServiceContext _context;
        public LookupSchoolRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ICollection<School>> SearchAsync(string name)    
        {
            IQueryable<School> query = _context.LookupSchools;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(q => q.Name.ToLower().Contains(name.ToLower()));
            }
            
            return await query.ToListAsync();
        }
    }
}