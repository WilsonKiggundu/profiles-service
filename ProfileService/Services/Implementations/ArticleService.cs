using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Blog.Article;
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

        public IEnumerable<GetArticle> GetAll()
        {
            return _mapper.Map<ICollection<GetArticle>>(_repository.GetAll());
        }

        public async Task<GetArticle> GetByIdAsync(Guid id)
        {
            var comment = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetArticle>(comment);
        }

        public async Task InsertAsync(NewArticle comment)
        {

            await _repository.InsertAsync(_mapper.Map<Article>(comment));
        }

        public async Task UpdateAsync(UpdateArticle comment)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}