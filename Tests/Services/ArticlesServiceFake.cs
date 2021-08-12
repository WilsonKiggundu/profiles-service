using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Article;
using ProfileService.Models.Posts;
using ProfileService.Services.Interfaces;

namespace Tests.Services
{
    public class ArticlesServiceFake : IArticleService
    {
        private readonly List<Article> _articles;
        public ArticlesServiceFake()
        {
            _articles = new List<Article>
            {
                new Article
                {
                    AuthorId = Guid.NewGuid(),
                    Id = Guid.NewGuid(),
                    Title = "What is Engineering as a Service?",
                    Summary = "This is a summary",
                    Details = "These are details of the post"
                }
            };
        }
        public async Task<SearchArticleResponse> SearchAsync(SearchArticleRequest request)
        {
            return new SearchArticleResponse
            {
                Articles = _articles,
                Request = request,
                HasMore = false
            };
        }

        public async Task<GetArticle> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(NewArticle article)
        {
            var insert = new Article
            {
                Id = Guid.NewGuid(),
                AuthorId = article.AuthorId,
                Details = article.Details,
                Summary = article.Summary
            };
            
            _articles.Add(insert);
        }

        public async Task UpdateAsync(UpdateArticle article)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}