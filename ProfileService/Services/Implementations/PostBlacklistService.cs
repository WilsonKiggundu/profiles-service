using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Models.Preferences;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class PostBlacklistService : IPostBlacklistService
    {
        private readonly IPostBlacklistRepository _repository;

        public PostBlacklistService(IPostBlacklistRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PostBlacklist>> GetByIdAsync(Guid personId)
        {
            return await _repository.GetByPersonIdAsync(personId);
        }

        public async Task InsertAsync(PostBlacklist entity)
        {
            await _repository.InsertAsync(entity);
        }

        public async Task DeleteAsync(Guid personId, Guid postId)
        {
            await _repository.DeleteAsync(personId, postId);
        }
    }
}