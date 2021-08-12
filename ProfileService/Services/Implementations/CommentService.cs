using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
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

        public CommentService(ICommentRepository repository, IMapper mapper, IPersonRepository personRepository,
            IWebNotification notification, IDeviceService deviceService, IPostRepository postRepository,
            ILogger<CommentService> logger, IConfiguration configuration)
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
            
            var author = await _personRepository.GetByIdAsync(comment.AuthorId);
            entity.Author = author;

            BackgroundJob.Enqueue(() => NotifyAsync(entity));

            return _mapper.Map<NewComment>(entity);
        }

        public async Task NotifyAsync(Comment comment)
        {
            comment.Author = await _personRepository.GetByIdAsync(comment.AuthorId);

            if (comment.PostId.HasValue)
            {
                var post = await _postRepository.GetByIdAsync(comment.PostId.Value);

                // don't send a notification if I comment on my own post
                var excludeMe = comment.AuthorId == post.AuthorId ? post.AuthorId.ToString() : string.Empty;

                var devices
                    = await _deviceService.SearchAsync(excludeMe, post.AuthorId.ToString());

                _notification.Send(devices, new NotificationPayload
                {
                    Title = $"{comment.Author.Firstname} {comment.Author.Lastname}" + " commented on your post",
                    Message = comment.Details[..(comment.Details.Length < 60 ? comment.Details.Length : 60)],
                    Icon = comment.Author.Avatar,
                    Date = DateTime.UtcNow,

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
                        Icon = comment.Author.Avatar,
                    }
                });
            }
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