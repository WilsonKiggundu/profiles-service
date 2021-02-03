using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProfileService.Contracts.Lookup.Category;
using ProfileService.Models.Common;
using ProfileService.Models.Person;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class LookupCategoryService : ILookupCategoryService
    {
        private readonly ILookupCategoryRepository _repository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private ILogger<LookupCategoryService> _logger;

        public LookupCategoryService(ILookupCategoryRepository repository, IMapper mapper, ILogger<LookupCategoryService> logger, IPersonRepository personRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _personRepository = personRepository;
        }

        public ICollection<GetLookupCategory> SearchAsync()
        {
            var categories =  _repository.GetAll();
            return _mapper.Map<ICollection<GetLookupCategory>>(categories);
        }

        public async Task<GetLookupCategory> GetByIdAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetLookupCategory>(category);
        }

        public async Task<NewLookupCategory> InsertAsync(NewLookupCategory category)
        {
            try
            {
                var model = _mapper.Map<Category>(category);
                await _repository.InsertAsync(model);

                if (category.PersonId.HasValue)
                {
                    await _personRepository.AddCategoryAsync(new PersonCategory
                    {
                        CategoryId = model.Id,
                        PersonId = category.PersonId.Value
                    });
                }

                category.Id = model.Id;
                return category;
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