using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Blog.Comment;
using ProfileService.Helpers;
using ProfileService.Models.Common;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IPersonRepository _personRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IDeviceService _deviceService;
        private readonly IWebNotification _notification;

        public CommentService(ICommentRepository repository, IMapper mapper, IPersonRepository personRepository, IWebNotification notification, IDeviceService deviceService, IPostRepository postRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _personRepository = personRepository;
            _notification = notification;
            _deviceService = deviceService;
            _postRepository = postRepository;
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

            if (comment.PostId.HasValue)
            {
                var post = await _postRepository.GetByIdAsync(comment.PostId.Value);
                var device = await _deviceService.SearchAsync(post.Author.Id.ToString());
                await _notification.SendAsync(new List<Device>(device), new NotificationPayload
                {
                    Title = entity.Author.Firstname + " commented on your post",
                    Message = comment.Details,
                    Data = new
                    {
                        postId = comment.PostId,
                        commentId = comment.Id
                    },
                    Options = new NotificationOptions
                    {
                        Actions = new List<NotificationAction>
                        {
                            new NotificationAction
                            {
                                Action = "view-profile",
                                Title = "View profile"
                            },
                            new NotificationAction
                            {
                                Action = "follow",
                                Title = "Follow"
                            }
                        },
                        Body = comment.Details,
                        Tag = comment.Id.ToString(),
                        Icon = entity.Author.Avatar,
                    }
                });
            }
            
            
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