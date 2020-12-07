using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<GetPost> GetAll()
        {
            return _mapper.Map<ICollection<GetPost>>(_repository.GetAll());
        }

        public async Task<GetPost> GetByIdAsync(Guid id)
        {
            var comment = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetPost>(comment);
        }

        public async Task InsertAsync(NewPost comment)
        {

            await _repository.InsertAsync(_mapper.Map<Post>(comment));
        }

        public async Task UpdateAsync(UpdatePost comment)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}