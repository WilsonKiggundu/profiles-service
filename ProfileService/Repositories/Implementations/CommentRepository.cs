using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Blog.Comment;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly ProfileServiceContext _context;
        private readonly ILogger<CommentRepository> _logger;
        
        public CommentRepository(ProfileServiceContext context, ILogger<CommentRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<SearchCommentsResponse> SearchAsync(SearchCommentsRequest filter)
        {
            IQueryable<Comment> query = _context.Comments
                .Include(c => c.Author)
                .OrderBy(q => q.DateCreated);
            
            if (filter.ArticleId.HasValue)
            {
                query = query.Where(c => c.ArticleId == filter.ArticleId);
            }
            
            if (filter.PostId.HasValue)
            {
                query = query.Where(c => c.PostId == filter.PostId);
            }

            var offset = (filter.Page - 1) * filter.PageSize;
            var hasMore = await query.Skip(offset).CountAsync() > 0;
            
            var comments = await query
                .Skip((filter.Page - 1) * filter.Page)
                .Take(filter.PageSize).ToListAsync();
            
            return new SearchCommentsResponse
            {
                PostId = filter.PostId,
                ArticleId = filter.ArticleId,
                Page = filter.Page,
                PageSize = filter.PageSize,
                Comments = comments,
                HasMore = hasMore
            };
        }
    }
}