using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Article;

namespace ProfileService.Services.Interfaces
{
    public interface IArticleService : IService
    {
        IEnumerable<GetArticle> GetAll();
        Task<GetArticle> GetByIdAsync(Guid id);
        Task InsertAsync(NewArticle article);
        Task UpdateAsync(UpdateArticle article);
        Task DeleteAsync(Guid id);
    }
}