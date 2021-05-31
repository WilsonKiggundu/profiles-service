using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models.Preferences;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class PostBlacklistRepository : IPostBlacklistRepository
    {
        private readonly ProfileServiceContext _context;

        public PostBlacklistRepository(ProfileServiceContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Guid personId, Guid postId)
        {
            var toDelete = await _context
                .PostBlacklists
                .FirstOrDefaultAsync(q => 
                    q.PersonId == personId && 
                    q.BlacklistId == postId);

            _context.PostBlacklists.Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PostBlacklist> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PostBlacklist> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(PostBlacklist entity)
        {
            await _context.PostBlacklists.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertManyAsync(ICollection<PostBlacklist> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(PostBlacklist entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<PostBlacklist>> GetByPersonIdAsync(Guid personId)
        {
            return await _context
                .PostBlacklists
                .Where(p => p.PersonId == personId)
                .ToListAsync();
        }
    }
}