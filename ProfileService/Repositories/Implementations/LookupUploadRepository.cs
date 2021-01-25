using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Lookup.Upload;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class LookupUploadRepository : GenericRepository<Upload>, ILookupUploadRepository
    {
        private readonly ProfileServiceContext _context;
        public LookupUploadRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ICollection<Upload>> SearchAsync(SearchLookupUpload request)
        {
            IQueryable<Upload> uploads = _context.LookupUploads;

            if (request.OwnerId != null)
            {
                uploads = uploads.Where(q => q.OwnerId.Equals(request.OwnerId.Value));
            }

            if (!string.IsNullOrEmpty(request.FileName))
            {
                uploads = uploads.Where(q => q.FileName.ToLower().Contains(request.FileName.ToLower()));
            }

            return await uploads.ToListAsync();
        }
    }
}