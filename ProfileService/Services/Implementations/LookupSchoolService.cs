#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class LookupSchoolService : ILookupSchoolService
    {
        private readonly IMapper _mapper;
        private readonly ILookupSchoolRepository _repository;

        public LookupSchoolService(IMapper mapper, ILookupSchoolRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ICollection<School>> SearchAsync(string name)
        {
            var schools = await _repository.SearchAsync(name);
            return _mapper.Map<ICollection<School>>(schools);
        }


        public async Task<School> GetByIdAsync(Guid id)
        {
            var school = await _repository.GetByIdAsync(id);
            return _mapper.Map<School>(school);
        }

        public async Task InsertAsync(School school)
        {
            try
            {
                var model = _mapper.Map<School>(school);
                await _repository.InsertAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        public async Task InsertManyAsync(ICollection<School> schools)
        {
            try
            {
                var model = _mapper.Map<ICollection<School>>(schools);
                await _repository.InsertManyAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAsync(School school)
        {
            try
            {
                var model = _mapper.Map<School>(school);
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