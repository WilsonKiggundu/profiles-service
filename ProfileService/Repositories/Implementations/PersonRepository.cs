using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Person;
using ProfileService.Data;
using ProfileService.Models;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private readonly ProfileServiceContext _context;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public PersonRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Search persons
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ICollection<Person>> SearchAsync(SearchPerson request)
        {
            return await _context.Persons.ToListAsync();
        }
    }
}