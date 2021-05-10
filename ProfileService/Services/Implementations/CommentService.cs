using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private readonly IConfiguration _configuration;
        private readonly ILogger<CommentService> _logger;

        public CommentService(ICommentRepository repository, IMapper mapper, IPersonRepository personRepository, IWebNotification notification, IDeviceService deviceService, IPostRepository postRepository, ILogger<CommentService> logger, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _personRepository = personRepository;
            _notification = notification;
            _deviceService = deviceService;
            _postRepository = postRepository;
            _logger = logger;
            _configuration = configuration;
        }
        
        public async Task<SearchCommentsResponse> SearchAsync(SearchCommentsRequest filter)
        {
            return await _repository.SearchAsync(filter);
        }

        public async Task<NewComment> InsertAsync(NewComment comment)
        {
            comment.Id = Guid.NewGuid();
            
            var entity = _mapper.Map<Comment>(comment);
            await _repository.InsertAsync(entity);

            entity.Author = await _personRepository.GetByIdAsync(comment.AuthorId);

            if (comment.PostId.HasValue)
            {
                var post = await _postRepository.GetByIdAsync(comment.PostId.Value);
                var devices 
                    = await _deviceService.SearchAsync(null, post.AuthorId.ToString());
                
                _notification.Send(devices, new NotificationPayload
                {
                    Title = entity.Author.Firstname + " commented on your post",
                    Message = comment.Details,
                    
                    Data = new
                    {
                        postId = comment.PostId,
                        commentId = comment.Id,
                        profileId = comment.AuthorId,
                        baseUrl = _configuration.GetSection("MyVillageBaseUrl").Get<string>()
                    },
                    
                    Options = new NotificationOptions
                    {
                        Actions = new List<NotificationAction>
                        {
                            new NotificationAction
                            {
                                Action = "view-profile",
                                Title = "View profile"
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