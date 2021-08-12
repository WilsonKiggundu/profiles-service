using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Contracts.Person;
using ProfileService.Helpers;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

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
            var post = new Post
            {
                Id = Guid.NewGuid(),
                AuthorId = newPost.AuthorId,
                Details = newPost.Details,
                Uploads = newPost.Uploads,
                Type = newPost.Type,
                Ref = newPost.Ref,
                ReferenceId = newPost.ReferenceId,
                Title = newPost.Title
            };

            await _repository.InsertAsync(post);
            var author = await _personRepository.GetByIdAsync(newPost.AuthorId);
            
            newPost.Author = new GetPerson
            {
                Avatar = author.Avatar,
                Email = author.Email,
                Firstname = author.Firstname,
                Lastname = author.Lastname,
                Id = author.Id
            };

            BackgroundJob.Enqueue(() => SendNotification(post));

            return newPost;
        }

        public async Task SendNotification(Post post)
        {
            post = await _repository.GetByIdAsync(post.Id);
            post.Author = await _personRepository.GetByIdAsync(post.AuthorId);

            try
            {
                var payload = new NotificationPayload
                {
                    Title = $"{post.Author.Firstname} {post.Author.Lastname} posted something",
                    Message = post.Details,
                    Data = new
                    {
                        profileId = post.Author.Id
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
                            // new NotificationAction
                            // {
                            //     Action = "follow",
                            //     Title = "Follow"
                            // }
                        },
                        Body = post.Details,
                        Tag = post.Id.ToString(),
                        Icon = post.Author.Avatar,
                    }
                };

                var devices = await _devices.SearchAsync(post.AuthorId.ToString());
                _notification.Send(devices, payload);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }
        }

        public async Task UpdateAsync(UpdatePost entity)
        {
            var post = _mapper.Map<Post>(entity);
            await _repository.UpdateAsync(post);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}