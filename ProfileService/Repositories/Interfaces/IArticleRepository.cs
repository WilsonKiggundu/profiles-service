using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Article;
using ProfileService.Models.Posts;

namespace ProfileService.Repositories.Interfaces
{
    public interface IArticleRepository : IGenericRepository<Article>
    {
        Task<SearchArticleResponse> SearchAsync(SearchArticleRequest request);
    }
}