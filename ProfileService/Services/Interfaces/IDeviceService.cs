using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models.Common;
using ProfileService.Models.Posts;

namespace ProfileService.Services.Interfaces
{
    public interface IDeviceService : IService    
    {        
        Task<ICollection<Device>> SearchAsync(string except = null, string name = null);
        Task InsertAsync(Device device);
        Task DeleteAsync(Guid id);
        Task<Device> GetByName(string name);

        Task<VapidKeys> GetVapidKeysAsync();
    }
}