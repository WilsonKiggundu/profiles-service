using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models.Preferences;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class PersonPreferencesRepository : IPersonPreferencesRepository
    {
        private readonly ProfileServiceContext _context;

        public PersonPreferencesRepository(ProfileServiceContext context)
        {
            _context = context;
        }

        public IEnumerable<EmailSettings> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<EmailSettings> GetByIdAsync(Guid id)
        {
            return await _context.EmailPreferences
                .FirstOrDefaultAsync(d => d.PersonId == id);
        }

        public async Task InsertAsync(EmailSettings settings)
        {
            await _context.EmailPreferences.AddAsync(settings);
            await _context.SaveChangesAsync();
        }

        public async Task InsertManyAsync(ICollection<EmailSettings> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(EmailSettings settings)
        {
            _context.EmailPreferences.Update(settings);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(Guid personId)
        {
            return await _context.EmailPreferences.AnyAsync(q => q.PersonId == personId);
        }
    }
}