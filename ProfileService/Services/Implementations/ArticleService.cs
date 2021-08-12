using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using ProfileService.Contracts.Blog.Article;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Common;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public ArticleService(IArticleRepository repository, IMapper mapper, IPostService postService)
        {
            _repository = repository;
            _mapper = mapper;
            _postService = postService;
        }

        public async Task<SearchArticleResponse> SearchAsync(SearchArticleRequest request)
        {
            return await _repository.SearchAsync(request);
        }

        public async Task<GetArticle> GetByIdAsync(Guid id)
        {
            var article = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetArticle>(article);
        }

        public async Task InsertAsync(NewArticle article)
        {
            var newArticle = new Article
            {
                Id = Guid.NewGuid(),
                Title = article.Title,
                Details = article.Details,
                AuthorId = article.AuthorId,
                Uploads = article.Uploads?.Select(s => new Upload
                {
                    Path = s.Path,
                    FileName = s.FileName
                }).ToList(),
                Status = article.Status,
                // Categories = article.Categories?.Select(s =>
                // {
                //     var isGuid = Guid.TryParse(s, out Guid id);
                //     if (isGuid)
                //     {
                //         return new ArticleCategory
                //         {
                //             Id = id,
                //         };
                //     }
                //     
                //     
                // }).ToList(),
                // Tags = article.Tags
            };
            
            await _repository.InsertAsync(newArticle);

            var newPost = new NewPost
            {
                Type = PostType.Article,
                AuthorId = article.AuthorId,
                Title = article.Title,
                Details = article.Details,
                ReferenceId = newArticle.Id,
                Uploads = article.Uploads?.Select(s => new Upload
                {
                    Path = s.Path,
                    FileName = s.FileName
                }).ToList()
            };
            
            BackgroundJob.Enqueue(() => _postService.InsertAsync(newPost));

        }

        public async Task UpdateAsync(UpdateArticle article)
        {
            var model = _mapper.Map<Article>(article);
            await _repository.UpdateAsync(model);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}