using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Lookup.Upload;

namespace ProfileService.Services.Interfaces
{
    public interface ILookupUploadService : IService
    {
        Task<ICollection<GetLookupUpload>> SearchAsync(SearchLookupUpload request);
        Task<GetLookupUpload> GetByIdAsync(Guid id);
        Task InsertAsync(NewLookupUpload upload);
        Task UpdateAsync(UpdateLookupUpload upload);
        Task DeleteAsync(Guid id);
    }
}