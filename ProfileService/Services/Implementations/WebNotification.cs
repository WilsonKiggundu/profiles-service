using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProfileService.Helpers;
using ProfileService.Models.Common;
using ProfileService.Services.Interfaces;
using WebPush;

namespace ProfileService.Services.Implementations
{
    public class WebNotification : IWebNotification
    {
        private readonly IDeviceService _deviceService;

        public WebNotification(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public async Task SendAsync(ICollection<Device> devices, NotificationPayload payload)
        {
            var vapidKeys = await _deviceService.GetVapidKeysAsync();
            
            devices.ToList().ForEach(async device =>
            {
                var pushSubscription = new PushSubscription(device.PushEndpoint, device.PushP256DH, device.PushAuth);
                var vapidDetails = new VapidDetails("mailto:no-reply@myvillage.africa", vapidKeys.PublicKey,
                    vapidKeys.PrivateKey);
                
                var webPushClient = new WebPushClient();

                await webPushClient.SendNotificationAsync(pushSubscription, JsonConvert.SerializeObject(payload), vapidDetails);
            });

        }
    }
}