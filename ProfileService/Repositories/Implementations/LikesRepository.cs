using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class LikesRepository : GenericRepository<Like>, ILikesRepository
    {
        private readonly ProfileServiceContext _context;
        public LikesRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SearchLikeResponse> SearchAsync(SearchLikeRequest request)
        {
            var query = 
                _context
                    .Likes
                    .Where(p => p.EntityId == request.PostId);
            
            var skip = (request.Page - 1) * request.PageSize;
            var hasMore = await query.Skip(skip).CountAsync() > 0;

            var likes = await query
                .Include(l => l.Person)
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync();
            
            return new SearchLikeResponse
            {
                Likes = likes,
                Request = request,
                HasMore = hasMore
            };
        }
    }
}