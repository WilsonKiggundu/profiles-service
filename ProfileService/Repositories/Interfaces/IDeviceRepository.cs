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
        Task<ICollection<Device>> SearchAsync(string name = null);
        Task<VapidKeys> GetVapidKeysAsync();
    }

    
}