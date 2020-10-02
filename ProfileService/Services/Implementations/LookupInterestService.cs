using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class LookupInterestService : ILookupInterestService
    {
        private readonly IMapper _mapper;
        private readonly ILookupInterestRepository _repository;

        public LookupInterestService(IMapper mapper, ILookupInterestRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ICollection<GetLookupInterest>> SearchAsync(SearchLookupInterest request)
        {
            var interests = await _repository.SearchAsync(request);
            return _mapper.Map<ICollection<GetLookupInterest>>(interests);
        }

        public async Task<GetLookupInterest> GetByIdAsync(Guid id)
        {
            var interest = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetLookupInterest>(interest);
        }

        public async Task InsertAsync(NewLookupInterest interest)
        {
            try
            {
                var model = _mapper.Map<Interest>(interest);
                await _repository.InsertAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAsync(UpdateLookupInterest interest)
        {
            try
            {
                var model = _mapper.Map<Interest>(interest);
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