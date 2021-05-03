using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Article> Search(int page = 1, int limit= 50)
        {
            return _context.Articles.Skip((page - 1)*limit).Take(limit).ToList();
        }
    }
}