using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class PostRepository : IPostRepository
    {
        private readonly ProfileServiceContext _context;

        public PostRepository(ProfileServiceContext context)
        {
            _context = context;
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Uploads)
                .Include(p => p.Comments)
                .OrderByDescending(q => q.DateCreated)
                .ToList();
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            return await _context.Posts
                .Include(p => p.Author)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task InsertAsync(Post entity)
        {
            await _context.Posts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertManyAsync(ICollection<Post> entities)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Post entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}