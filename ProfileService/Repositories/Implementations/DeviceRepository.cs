using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        private readonly ProfileServiceContext _context;

        public DeviceRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Device> GetByName(string profileId)
        {
            return await _context
                .Devices
                .FirstOrDefaultAsync(q => q.ProfileId.Equals(profileId));
        }

        public async Task<bool> IsRegistered(string profileId, string token)
        {
            return await _context.Devices
                .AnyAsync(d =>
                    d.ProfileId.Equals(profileId) &&
                    d.Token.Equals(token));
        }

        public async Task<ICollection<Device>> SearchAsync(string except, string profileId)
        {
            IQueryable<Device> query = _context.Devices;

            if (!string.IsNullOrEmpty(except))
            {
                query = query.Where(q => !q.ProfileId.Equals(except));
            }

            if (!string.IsNullOrEmpty(profileId))
            {
                query = query.Where(q => q.ProfileId == profileId);
            }

            return await query.ToListAsync();
        }

        public async Task<VapidKeys> GetVapidKeysAsync()
        {
            return await _context.VapidKeys.SingleOrDefaultAsync();
        }
    }
}