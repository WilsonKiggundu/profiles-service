using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Category;
using ProfileService.Models.Common;

namespace ProfileService.Repositories.Interfaces
{
    public interface ILookupCategoryRepository : IGenericRepository<Category>
    {
        Task<ICollection<Category>> SearchAsync(SearchLookupCategory request);
    }
}