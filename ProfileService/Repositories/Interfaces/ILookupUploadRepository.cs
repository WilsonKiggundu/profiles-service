using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Models.Common;

namespace ProfileService.Repositories.Interfaces
{
    public interface ILookupUploadRepository : IGenericRepository<Upload>
    {
        Task<ICollection<Upload>> SearchAsync(SearchLookupUpload request);
    }
}