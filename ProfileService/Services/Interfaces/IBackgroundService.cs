using System.Threading.Tasks;

namespace ProfileService.Services.Interfaces
{
    public interface IBackgroundService : IService
    {
        Task SendProfileUpdateRemindersAsync();
    }
}