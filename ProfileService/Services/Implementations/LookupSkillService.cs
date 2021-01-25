#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Contracts.Lookup.Skill;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class LookupSkillService : ILookupSkillService
    {
        private readonly IMapper _mapper;
        private readonly ILookupSkillRepository _repository;

        public LookupSkillService(IMapper mapper, ILookupSkillRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ICollection<GetLookupSkill>> SearchAsync()
        {
            var skills = await _repository.SearchAsync();
            return _mapper.Map<ICollection<GetLookupSkill>>(skills);
        }


        public async Task<GetLookupSkill> GetByIdAsync(Guid id)
        {
            var skill = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetLookupSkill>(skill);
        }

        public async Task InsertAsync(NewLookupSkill skill)
        {
            try
            {
                var model = _mapper.Map<Skill>(skill);
                await _repository.InsertAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        public async Task InsertManyAsync(ICollection<NewLookupSkill> skills)
        {
            try
            {
                var model = _mapper.Map<ICollection<Skill>>(skills);
                await _repository.InsertManyAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAsync(UpdateLookupSkill skill)
        {
            try
            {
                var model = _mapper.Map<Skill>(skill);
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