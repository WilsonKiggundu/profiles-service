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

        public async Task<Device> GetByName(string name)
        {
            return await _context
                .Devices
                .FirstOrDefaultAsync(q => q.Name.Equals(name));
        }

        public async Task<ICollection<Device>> SearchAsync(string except, string name)
        {
            IQueryable<Device> query = _context.Devices;

            if (!string.IsNullOrEmpty(except))
            {
                query = query.Where(q => !q.Name.Equals(except));
            }
            
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(q => q.Name.Equals(name));
            }

            return await query.ToListAsync();
        }

        public async Task<VapidKeys> GetVapidKeysAsync()
        {
            return await _context.VapidKeys.SingleOrDefaultAsync();
        }
    }
}