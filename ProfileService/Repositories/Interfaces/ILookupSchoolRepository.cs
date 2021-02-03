using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Models.Common;

namespace ProfileService.Repositories.Interfaces
{
    public interface ILookupSchoolRepository : IGenericRepository<School>
    {
        Task<ICollection<School>> SearchAsync(string name = null);        
    }
}