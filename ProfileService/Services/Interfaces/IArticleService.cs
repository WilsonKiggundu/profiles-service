using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Article;
using ProfileService.Models.Posts;

namespace ProfileService.Services.Interfaces
{
    public interface IArticleService : IService
    {
        Task<SearchArticleResponse> SearchAsync(SearchArticleRequest request);
        Task<GetArticle> GetByIdAsync(Guid id);
        Task InsertAsync(NewArticle article);
        Task UpdateAsync(UpdateArticle article);
        Task DeleteAsync(Guid id);
    }
}