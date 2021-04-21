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

        public async Task<ICollection<Device>> SearchAsync(string except = null, string name = null)
        {
            return await _repository.SearchAsync(name);    
        }

        public async Task InsertAsync(Device device)
        {
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