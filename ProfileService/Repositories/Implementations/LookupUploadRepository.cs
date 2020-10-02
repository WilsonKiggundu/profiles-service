using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Data;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class LookupUploadRepository : GenericRepository<Upload>, ILookupUploadRepository
    {
        public LookupUploadRepository(ProfileServiceContext context) : base(context) { }
        public async Task<ICollection<Upload>> SearchAsync(SearchLookupUpload request)
        {
            throw new System.NotImplementedException();
        }
    }
}