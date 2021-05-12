using System.Threading.Tasks;
using ProfileService.Contracts;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models;
using ProfileService.Models.Common;
using ProfileService.Models.Posts;

namespace ProfileService.Repositories.Interfaces
{
    public interface IEmailTemplateRepository : IGenericRepository<EmailTemplate>
    {
        Task<EmailTemplate> GetByType(EmailType type);
    }
}