using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Common;
using ProfileService.Models.Posts;

namespace ProfileService.Repositories.Interfaces
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        Task<Device> GetByName(string name);
        Task<bool> IsRegistered(string profileId, string token);
        Task<ICollection<Device>> SearchAsync(string except = null, string name = null);
        Task<ICollection<Device>> SearchAsync(List<Guid> deviceIds);
        Task<VapidKeys> GetVapidKeysAsync();
    }

    
}