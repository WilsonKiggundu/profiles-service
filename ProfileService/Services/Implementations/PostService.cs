using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Helpers;
using ProfileService.Models.Common;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;
using WebPush;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ProfileService.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;
        private readonly IWebNotification _notification;
        private readonly IDeviceService _devices;

        public PostService(IPostRepository repository, IMapper mapper, ILogger<PostService> logger,
            IPersonRepository personRepository, IWebNotification notification, IDeviceService devices)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _personRepository = personRepository;
            _notification = notification;
            _devices = devices;
        }

        public async Task<SearchPostResponse> SearchAsync(SearchPostRequest request)
        {
            return await _repository.SearchAsync(request);
        }

        public async Task<NewPost> InsertAsync(NewPost newPost)
        {
            ICollection<Upload> uploads = null;

            if (!string.IsNullOrEmpty(newPost.Uploads))
            {
                uploads = JsonSerializer.Deserialize<ICollection<Upload>>(newPost.Uploads, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }

            var post = new Post
            {
                Id = Guid.NewGuid(),
                AuthorId = newPost.AuthorId,
                Details = newPost.Details,
                Uploads = uploads
            };

            await _repository.InsertAsync(post);

            post = await _repository.GetByIdAsync(post.Id);
            post.Author = await _personRepository.GetByIdAsync(post.AuthorId);

            var result = _mapper.Map<NewPost>(post);
            result.Uploads = newPost.Uploads;

            try
            {
                var payload = new NotificationPayload
                {
                    Title = "New post added",
                    Message = post.Details,
                    Data = new
                    {
                        authorId = post.Author.Id  
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
                        Body = post.Details,
                        Tag = post.Id.ToString(),
                        Icon = post.Author.Avatar,
                    }
                };

                var devices = await _devices.SearchAsync();
                Task.Run(() => _notification.SendAsync(devices, payload));

            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return result;
        }
    }
}