using System;
using System.Threading.Tasks;
using ProfileService.Models;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _repository;

        public EmailTemplateService(IEmailTemplateRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmailTemplate> GetByTypeAsync(EmailType type)
        {
            return await _repository.GetByType(type);
        }

        public async Task<EmailTemplate> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task InsertAsync(EmailTemplate template)
        {
            await _repository.InsertAsync(template);
        }

        public async Task UpdateAsync(EmailTemplate template)
        {
            await _repository.UpdateAsync(template);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}