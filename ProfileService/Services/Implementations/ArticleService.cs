using System;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Blog.Article;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;
        private readonly IMapper _mapper;

        public ArticleService(IArticleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            await _repository.InsertAsync(_mapper.Map<Article>(article));
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