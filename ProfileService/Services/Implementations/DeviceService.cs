using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProfileService.Contracts;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repository;
        private readonly ILogger<DeviceService> _logger;

        public DeviceService(IDeviceRepository repository, ILogger<DeviceService> logger)
        {
            _repository = repository;
            _logger = logger;    
        }

        public async Task<ICollection<Device>> SearchAsync(string except = null, string profileId = null)
        {
            return await _repository.SearchAsync(except, profileId);    
        }

        public async Task<ICollection<Device>> SearchAsync(List<Guid> deviceIds)
        {
            return await _repository.SearchAsync(deviceIds);    
        }

        public async Task InsertAsync(Device device)
        {
            var alreadyRegistered = await _repository.IsRegistered(device.ProfileId, device.Token);
            if (alreadyRegistered)
            {
                return;
            }
            await _repository.InsertAsync(device);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<Device> GetByName(string name)
        {
            return await _repository.GetByName(name);
        }

        public async Task<VapidKeys> GetVapidKeysAsync()
        {
            return await _repository.GetVapidKeysAsync();
        }
    }
}