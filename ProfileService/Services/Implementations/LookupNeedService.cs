using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Lookup.Need;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class LookupNeedService : ILookupNeedService
    {
        private readonly IMapper _mapper;
        private readonly ILookupNeedRepository _repository;

        public LookupNeedService(IMapper mapper, ILookupNeedRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ICollection<GetLookupNeed>> SearchAsync(SearchLookupNeed request)
        {
            var needs = await _repository.SearchAsync(request);
            return _mapper.Map<ICollection<GetLookupNeed>>(needs);
        }

        public async Task<GetLookupNeed> GetByIdAsync(Guid id)
        {
            var need = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetLookupNeed>(need);
        }

        public async Task InsertAsync(NewLookupNeed need)
        {
            try
            {
                var model = _mapper.Map<Need>(need);
                await _repository.InsertAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAsync(UpdateLookupNeed need)
        {
            try
            {
                var model = _mapper.Map<Need>(need);
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