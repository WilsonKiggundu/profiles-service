using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Blog.Article;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class ArticleRepository : GenericRepository<Article>,  IArticleRepository
    {
        private readonly ProfileServiceContext _context;

        public ArticleRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SearchArticleResponse> SearchAsync(SearchArticleRequest request)
        {
            IQueryable<Article> query = _context.Articles
                .Include(q => q.Uploads)
                .Include(q => q.Author)
                .Where(q => !q.IsDeleted)
                .OrderByDescending(q => q.DateCreated);

            if (request.ArticleId.HasValue)
            {
                query = query.Where(q => q.Id.Equals(request.ArticleId.Value));
            }

            if (request.AuthorId.HasValue)
            {
                query = query.Where(q => q.AuthorId.Equals(request.AuthorId.Value));
            }

            if (!string.IsNullOrEmpty(request.Title))
            {
                query = query.Where(q => q.Title.ToLower().Contains(request.Title.ToLower()));
            }
            
            var skip = (request.Page - 1) * request.PageSize;
            var hasMore = await query.Skip(skip).CountAsync() > 0;

            var articles = await query
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync();

            return new SearchArticleResponse
            {
                Articles = articles,
                HasMore = hasMore,
                Request = request
            };
        }
    }
}