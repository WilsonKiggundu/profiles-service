using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models.Preferences;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class PersonBlacklistRepository : IPersonBlacklistRepository
    {
        private readonly ProfileServiceContext _context;

        public PersonBlacklistRepository(ProfileServiceContext context)
        {
            _context = context;
        }
        
        public async Task DeleteAsync(Guid personId, Guid blacklistId)
        {
            var toDelete = await _context
                .PersonBlacklists
                .FirstOrDefaultAsync(q => 
                    q.PersonId == personId &&
                    q.BlacklistId == blacklistId);

            _context.PersonBlacklists.Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PersonBlacklist> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PersonBlacklist> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(PersonBlacklist entity)
        {
            await _context.PersonBlacklists.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertManyAsync(ICollection<PersonBlacklist> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(PersonBlacklist entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<PersonBlacklist>> GetByPersonIdAsync(Guid personId)
        {
            return await _context
                .PersonBlacklists
                .Where(p => p.PersonId == personId)
                .ToListAsync();
        }
    }
}