using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Blog.Article;
using ProfileService.Contracts.Preferences;
using ProfileService.Models.Posts;
using ProfileService.Models.Preferences;

namespace ProfileService.Services.Interfaces
{
    public interface IPersonPreferenceService : IService
    {
        Task<EmailSettingsContract> GetByIdAsync(Guid id);
        Task UpdateAsync(UpdatePreferenceContract preference);
    }
}