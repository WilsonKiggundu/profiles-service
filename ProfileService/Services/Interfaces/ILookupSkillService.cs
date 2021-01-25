using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Contracts.Lookup.Skill;

namespace ProfileService.Services.Interfaces
{
    public interface ILookupSkillService : IService
    {
        Task<ICollection<GetLookupSkill>> SearchAsync();
        Task<GetLookupSkill> GetByIdAsync(Guid id);
        Task InsertAsync(NewLookupSkill skill);
        Task InsertManyAsync(ICollection<NewLookupSkill> skills);
        Task UpdateAsync(UpdateLookupSkill skill);
        Task DeleteAsync(Guid id);
    }
}