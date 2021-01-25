using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Models.Common;

namespace ProfileService.Repositories.Interfaces
{
    public interface ILookupSkillRepository : IGenericRepository<Skill>
    {
        Task<ICollection<Skill>> SearchAsync();    
    }
}