using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProfileService.Contracts.Lookup.Category;
using ProfileService.Data;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class LookupCategoryService : ILookupCategoryService
    {
        private readonly ILookupCategoryRepository _repository;
        private readonly IMapper _mapper;
        private ILogger<LookupCategoryService> _logger;

        public LookupCategoryService(ILookupCategoryRepository repository, IMapper mapper, ILogger<LookupCategoryService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<GetLookupCategory>> SearchAsync(SearchLookupCategory request)
        {
            var categories = await _repository.SearchAsync(request);
            return _mapper.Map<ICollection<GetLookupCategory>>(categories);
        }

        public async Task<GetLookupCategory> GetByIdAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetLookupCategory>(category);
        }

        public async Task InsertAsync(NewLookupCategory category)
        {
            try
            {
                var model = _mapper.Map<Category>(category);
                await _repository.InsertAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAsync(UpdateLookupCategory category)
        {
            try
            {
                _logger.LogInformation($"{category}");
                var model = _mapper.Map<Category>(category);
                await _repository.UpdateAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}