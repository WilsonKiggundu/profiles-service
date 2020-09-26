using Microsoft.EntityFrameworkCore;
using ProfileService.Models;

namespace ProfileService.Data
{
    /// <summary>
    /// Database Context
    /// </summary>
    public class ProfileServiceContext  : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public ProfileServiceContext(DbContextOptions<ProfileServiceContext> options) : base(options){}

        /// <summary>
        /// Persons
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Interests
        /// </summary>
        public DbSet<Interest> Interests { get; set; }

        /// <summary>
        /// Personal interests
        /// </summary>
        public DbSet<PersonInterest> PersonInterests { get; set; }

        /// <summary>
        /// Contacts
        /// </summary>
        public DbSet<Contact> Contacts { get; set; }
    }
}