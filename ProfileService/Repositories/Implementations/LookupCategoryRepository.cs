using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Category;
using ProfileService.Data;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class LookupCategoryRepository : GenericRepository<Category>, ILookupCategoryRepository
    {
        public LookupCategoryRepository(ProfileServiceContext context) : base(context) { }

        public async Task<ICollection<Category>> SearchAsync(SearchLookupCategory request)
        {
            throw new NotImplementedException();
        }
    }
}