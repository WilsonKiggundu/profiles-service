using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        private const string ServerKey = "AAAArGo6mug:APA91bHCYMJm3XQIyFV60uBInjWPLlD9okEhDcqCMO6EhL2FMzzBAvc4PLNrvyXsTbZHZ3m6XE1tqCLSjdh-5WQhY156BD3kgoKem0jM1Ol9RUtIBB5jEfPuYwhoy3DV60UWJDdMlF_a";
        private const string SenderId = "740516600552";
        private const string WebAddress = "https://fcm.googleapis.com/fcm/send";
        private readonly IDeviceService _deviceService;
        private readonly ILogger<WebNotification> _logger;

        public WebNotification(IDeviceService deviceService, ILogger<WebNotification> logger)
        {
            _deviceService = deviceService;
            _logger = logger;
        }
        
        public async Task SendAsync(List<Guid> deviceIds, NotificationPayload notification)
        {
            var devices = await _deviceService.SearchAsync(deviceIds);
            
            devices.ToList().ForEach(async device =>
            {
                var httpWebRequest = (HttpWebRequest) WebRequest.Create(WebAddress);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add($"Authorization: key={ServerKey}");
                httpWebRequest.Headers.Add($"Sender: id={SenderId}");
                httpWebRequest.Method = "POST";

                var payload = new
                {
                    to = device.Token,
                    priority = "high",
                    content_available = true,
                    notification
                };

                var json = JsonConvert.SerializeObject(payload);

                try
                {
                    await using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        await streamWriter.WriteAsync(json);
                        await streamWriter.FlushAsync();
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    var responseStream = httpResponse.GetResponseStream();
                    if (responseStream == null) return;
                    
                    using var streamReader = new StreamReader(responseStream);
                    await streamReader.ReadToEndAsync();

                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    _logger.LogError(e, e.Message);
                }

            });

        }

        public void Send(ICollection<Device> devices, NotificationPayload notification)
        {
            
            
            devices.ToList().ForEach(async device =>
            {
                // var httpWebRequest = (HttpWebRequest) WebRequest.Create(WebAddress);
                // httpWebRequest.ContentType = "application/json";
                // httpWebRequest.Headers.Add($"Authorization: key={ServerKey}");
                // httpWebRequest.Headers.Add($"Sender: id={SenderId}");
                // httpWebRequest.Method = "POST";
                //
                // var payload = new
                // {
                //     to = device.Token,
                //     priority = "high",
                //     content_available = true,
                //     notification
                // };
                //
                // var json = JsonConvert.SerializeObject(payload);
                //
                // try
                // {
                //     await using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                //     {
                //         await streamWriter.WriteAsync(json);
                //         await streamWriter.FlushAsync();
                //     }
                //
                //     var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //     var responseStream = httpResponse.GetResponseStream();
                //     if (responseStream == null) return;
                //     
                //     using var streamReader = new StreamReader(responseStream);
                //     await streamReader.ReadToEndAsync();
                //
                // }
                // catch (Exception e)
                // {
                //     Console.WriteLine();
                //     _logger.LogError(e, e.Message);
                // }

            });

        }
    }
}