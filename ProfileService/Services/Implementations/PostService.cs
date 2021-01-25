using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Common;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ProfileService.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(IPostRepository repository, IMapper mapper, ILogger<PostService> logger, IPersonRepository personRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _personRepository = personRepository;
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

            return result;
        }
    }
}