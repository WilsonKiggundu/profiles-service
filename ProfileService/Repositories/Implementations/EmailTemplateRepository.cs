using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly ProfileServiceContext _context;

        public EmailTemplateRepository(ProfileServiceContext context)
        {
            _context = context;
        }

        public IEnumerable<EmailTemplate> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<EmailTemplate> GetByIdAsync(Guid id)
        {
            return await _context.EmailTemplates.FindAsync(id);
        }

        public async Task InsertAsync(EmailTemplate template)
        {
            await _context.EmailTemplates.AddAsync(template);
            await _context.SaveChangesAsync();
        }

        public async Task InsertManyAsync(ICollection<EmailTemplate> templates)
        {
            await _context.EmailTemplates.AddRangeAsync(templates);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmailTemplate template)
        {
            _context.EmailTemplates.Update(template);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var template = await _context.EmailTemplates.FindAsync(id);
            _context.EmailTemplates.Remove(template);
            await _context.SaveChangesAsync();
        }

        public async Task<EmailTemplate> GetByType(EmailType type)
        {
            return await _context.EmailTemplates.FirstOrDefaultAsync(q => q.Type == type);
        }
    }
}