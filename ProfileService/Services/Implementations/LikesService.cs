using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Helpers;
using ProfileService.Models.Common;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class LikesService : ILikesService
    {
        private readonly ILikesRepository _repository;
        private readonly ILogger<LikesService> _logger;
        private readonly IPostRepository _postRepository;
        private readonly IWebNotification _notification;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IPersonRepository _personRepository;

        public LikesService(ILikesRepository repository, ILogger<LikesService> logger, IPostRepository postRepository,
            IDeviceRepository deviceRepository, IWebNotification notification, IPersonRepository personRepository)
        {
            _repository = repository;
            _logger = logger;
            _postRepository = postRepository;
            _deviceRepository = deviceRepository;
            _notification = notification;
            _personRepository = personRepository;
        }

        public async Task<SearchLikeResponse> SearchAsync(SearchLikeRequest request)
        {
            return await _repository.SearchAsync(request);
        }

        public async Task<Like> InsertAsync(Like like)
        {
            like.Id = Guid.NewGuid();

            _logger.LogInformation(JsonConvert.SerializeObject(like));

            await _repository.InsertAsync(like);

            like = await _repository.GetByIdAsync(like.Id);
            var person = await _personRepository.GetByIdAsync(like.PersonId);

            var post = (await _postRepository.SearchAsync(new SearchPostRequest
            {
                PostId = like.EntityId
            })).Posts.First();
            
            // don't send a notification if I comment on my own post
            var excludeMe = like.PersonId == post.AuthorId ? post.AuthorId.ToString() : string.Empty;
            
            var devices = await _deviceRepository.SearchAsync(excludeMe, post.AuthorId.ToString());
            
            _notification.Send(devices, new NotificationPayload
            {
                Title = $"{person.Firstname} {person.Lastname}" + " liked on your post",
                // Message = post.Details,
                Data = new
                {
                    postId = like.EntityId,
                    profileId = like.PersonId
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
                    // Body = comment.Details,
                    Tag = like.Id.ToString(),
                    Icon = person.Avatar,
                }
            });

            return like;
        }
    }
}