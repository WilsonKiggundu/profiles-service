using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Contact;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        private readonly ProfileServiceContext _context;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ContactRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Search Contacts
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ICollection<Contact>> SearchAsync(SearchContact request)
        {
            return await _context.Contacts.ToListAsync();
        }
    }
}