using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Helpers;
using ProfileService.Models.Common;

namespace ProfileService.Services.Interfaces
{
    public interface IWebNotification : IService
    {
        Task SendAsync(ICollection<Device> devices, NotificationPayload payload);
    }
}