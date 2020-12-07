using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Blog.Comment;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<GetComment> GetAll()
        {
            return _mapper.Map<ICollection<GetComment>>(_repository.GetAll());
        }
        
        public IEnumerable<GetComment> GetAll(Guid? postId, Guid? articleId)
        {
            return _mapper.Map<ICollection<GetComment>>(_repository.GetAll(postId, articleId));
        }

        public async Task<GetComment> GetByIdAsync(Guid id)
        {
            var comment = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetComment>(comment);
        }

        public async Task InsertAsync(NewComment comment)
        {

            await _repository.InsertAsync(_mapper.Map<Comment>(comment));
        }

        public async Task UpdateAsync(UpdateComment comment)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}