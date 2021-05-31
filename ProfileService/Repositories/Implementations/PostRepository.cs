using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class PostRepository : GenericRepository<Post>,  IPostRepository
    {
        private readonly ProfileServiceContext _context;

        public PostRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SearchPostResponse> SearchAsync(SearchPostRequest request)
        {
            var postBlacklist = await _context.PostBlacklists
                .Where(q => q.PersonId == request.UserId)
                .Select(s => s.BlacklistId)
                .ToListAsync();

            var personBlacklist = await _context.PersonBlacklists
                .Where(p => p.PersonId == request.UserId)
                .Select(s => s.BlacklistId)
                .ToListAsync();
            
            IQueryable<Post> query = _context.Posts
                .Where(p => !personBlacklist.Contains(p.AuthorId))
                .Where(p => !postBlacklist.Contains(p.Id))
                .OrderByDescending(p => p.DateCreated);

            if (request.PostId.HasValue)
            {
                request.PageSize = 1;
                request.Page = 1;
                query = query.Where(p => p.Id == request.PostId);
            }
            
            if (request.AuthorId.HasValue)
            {
                query = query.Where(p => p.AuthorId == request.AuthorId);
            }
            
            var skip = (request.Page - 1) * request.PageSize;
            var hasMore = await query.Skip(skip).CountAsync() > 0;

            var posts = await query
                .Include(p => p.Uploads)
                .Include(p => p.Author)
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync();
            
            posts.ForEach(post =>
            {
                post.Likes = new List<Like>();
                post.Comments = new List<Comment>();
                post.AlreadyLikedByUser = _context.Likes.Any(p => p.PersonId == request.UserId && p.EntityId == post.Id);
                post.CommentsCount = _context.Comments.Count(c => c.PostId == post.Id);
                post.LikesCount = _context.Likes.Count(c => c.EntityId == post.Id);
            });
            
            return new SearchPostResponse
            {
                Posts = posts,
                Request = request,
                HasMore = hasMore
            };

        }
    }
}