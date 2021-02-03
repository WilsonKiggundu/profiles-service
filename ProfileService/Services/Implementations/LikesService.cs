using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Posts;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class LikesService : ILikesService
    {
        private readonly ILikesRepository _repository;
        private readonly ILogger<LikesService> _logger;

        public LikesService(ILikesRepository repository, ILogger<LikesService> logger)
        {
            _repository = repository;
            _logger = logger;
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
            
            return like;
        }
    }
}