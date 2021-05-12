using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts;
using ProfileService.Contracts.Blog.Post;
using ProfileService.Models;
using ProfileService.Models.Common;
using ProfileService.Models.Posts;

namespace ProfileService.Services.Interfaces
{
    public interface IEmailTemplateService : IService    
    {        
        Task<EmailTemplate> GetByTypeAsync(EmailType type);    
        Task<EmailTemplate> GetByIdAsync(Guid id);    
        Task InsertAsync(EmailTemplate template);
        Task UpdateAsync(EmailTemplate template);
        Task DeleteAsync(Guid id);
    }
}