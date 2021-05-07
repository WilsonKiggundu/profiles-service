using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Models.Common;

namespace ProfileService.Repositories.Interfaces
{
    public interface ILookupStackRepository : IGenericRepository<TechStack>
    {
        Task<ICollection<TechStack>> SearchAsync(string name = null);            
    }
}