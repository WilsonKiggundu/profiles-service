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
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository repository, IMapper mapper, IPersonRepository personRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _personRepository = personRepository;
        }
        
        public async Task<SearchCommentsResponse> SearchAsync(SearchCommentsRequest filter)
        {
            return await _repository.SearchAsync(filter);
        }

        public async Task<NewComment> InsertAsync(NewComment comment)
        {
            var entity = _mapper.Map<Comment>(comment);
            await _repository.InsertAsync(entity);

            entity.Author = await _personRepository.GetByIdAsync(comment.AuthorId);
            
            return _mapper.Map<NewComment>(entity);
        }

        public async Task<UpdateComment> UpdateAsync(UpdateComment comment)
        {
            var entity = _mapper.Map<Comment>(comment);
            await _repository.UpdateAsync(entity);
            return comment;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}