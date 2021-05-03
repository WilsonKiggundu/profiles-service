using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ProfileService.Models.Business;
using ProfileService.Models.Common;
using ProfileService.Models.Investor;
using ProfileService.Models.Person;
using ProfileService.Models.Posts;
using ProfileService.Models.Preferences;

namespace ProfileService.Repositories
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Business>()
            //     .HasAlternateKey(b => b.Name)
            //     .HasName("AlternateKey_Name");

            modelBuilder.Entity<Category>()
                .HasQueryFilter(q => !q.IsDeleted);
            
            modelBuilder.Entity<Upload>()
                .HasQueryFilter(q => !q.IsDeleted);
            
            modelBuilder.Entity<Interest>()
                .HasQueryFilter(q => !q.IsDeleted);
            
            modelBuilder.Entity<Need>()
                .HasQueryFilter(q => !q.IsDeleted);

            modelBuilder.Entity<Like>()
                .HasKey(like => new {PostId = like.EntityId, like.PersonId});
                //.HasIndex(like => new {like.PostId, like.PersonId})
                //.IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        #region Common
        
        public DbSet<Interest> LookupInterests { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> LookupCategories { get; set; }
        public DbSet<Upload> LookupUploads { get; set; }
        public DbSet<Need> LookupNeeds { get; set; }    
        public DbSet<Skill> LookupSkills { get; set; }
        public DbSet<School> LookupSchools { get; set; }
        public DbSet<TechStack> LookupStacks { get; set; }
        
        #endregion
        
        #region Person
        
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonInterest> PersonInterests { get; set; }
        public DbSet<PersonSkill> PersonSkills { get; set; }
        public DbSet<PersonCategory> PersonCategories { get; set; }
        public DbSet<PersonAward> PersonAwards { get; set; }
        public DbSet<PersonConnection> PersonConnections { get; set; }
        public DbSet<PersonContact> PersonContacts { get; set; }    
        public DbSet<PersonEmployment> PersonEmploymentHistory { get; set; }
        public DbSet<PersonProject> PersonProjects { get; set; }
        public DbSet<PersonStack> DeveloperStack { get; set; }    

        #endregion

        #region Business

        public DbSet<Business> Businesses { get; set; }
        public DbSet<BusinessRole> BusinessRoles { get; set; }
        public DbSet<BusinessAddress> BusinessAddresses { get; set; }
        public DbSet<BusinessInterest> BusinessInterests { get; set; }
        public DbSet<BusinessNeed> BusinessNeeds { get; set; }
        public DbSet<BusinessProduct> BusinessProducts { get; set; }
        public DbSet<BusinessContact> BusinessContacts { get; set; }

        #endregion

        #region Investor

        public DbSet<Investor> Investors { get; set; }
        public DbSet<InvestorAddress> InvestorAddresses { get; set; }
        public DbSet<InvestorPortfolio> InvestorPortfolios { get; set; }
        public DbSet<InvestorInterest> InvestorInterests { get; set; }
        public DbSet<InvestorContact> InvestorContacts { get; set; }

        #endregion

        #region Posts

        public DbSet<Article> Articles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        
        #endregion

        #region Preferences

        public DbSet<EmailSettings> EmailPreferences { get; set; }

        #endregion

        #region Connected Devices

        public DbSet<Device> Devices { get; set; }
        public DbSet<VapidKeys> VapidKeys { get; set; }    

        #endregion
    }
}