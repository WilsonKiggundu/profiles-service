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
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(IPostRepository repository, IMapper mapper, ILogger<PostService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
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

        public async Task InsertAsync(NewPost newPost)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(newPost));

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
                AuthorId = newPost.AuthorId,
                Details = newPost.Details,
                Uploads = uploads
            };
            
            _logger.LogInformation(JsonConvert.SerializeObject(post));
            
            await _repository.InsertAsync(_mapper.Map<Post>(post));
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