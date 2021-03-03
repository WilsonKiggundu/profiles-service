using System.Threading.Tasks;
using ProfileService.Contracts.Common;

namespace ProfileService.Services.Interfaces
{
    public interface IEmailService : IService
    {
        Task SendEmailAsync(EmailDetails details);
    }
}