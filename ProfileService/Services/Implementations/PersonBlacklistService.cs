using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Models.Preferences;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class PersonBlacklistService : IPersonBlacklistService
    {
        private readonly IPersonBlacklistRepository _repository;

        public PersonBlacklistService(IPersonBlacklistRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PersonBlacklist>> GetByIdAsync(Guid personId)
        {
            return await _repository.GetByPersonIdAsync(personId);
        }

        public async Task InsertAsync(PersonBlacklist entity)
        {
            await _repository.InsertAsync(entity);
        }

        public async Task DeleteAsync(Guid personId, Guid blacklistId)
        {
            await _repository.DeleteAsync(personId, blacklistId);
        }
    }
}