using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<WebNotification> _logger;

        public WebNotification(IDeviceService deviceService, ILogger<WebNotification> logger)
        {
            _deviceService = deviceService;
            _logger = logger;
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

                try
                {
                    await webPushClient.SendNotificationAsync(pushSubscription, JsonConvert.SerializeObject(payload), vapidDetails);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                }

            });

        }
    }
}