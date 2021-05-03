#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class LookupSkillRepository : GenericRepository<Skill>, ILookupSkillRepository
    {
        private readonly ProfileServiceContext _context;
        public LookupSkillRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ICollection<Skill>> SearchAsync()
        {
            return await _context.LookupSkills.ToListAsync();
        }
    }
}