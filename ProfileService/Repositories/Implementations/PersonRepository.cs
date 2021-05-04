using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfileService.Contracts.Person;
using ProfileService.Models.Common;
using ProfileService.Models.Person;
using ProfileService.Models.Preferences;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private readonly ProfileServiceContext _context;
        private readonly ILogger<PersonRepository> _logger;
        private readonly ILookupCategoryRepository _lookupCategoryRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public PersonRepository(ProfileServiceContext context, ILogger<PersonRepository> logger, ILookupCategoryRepository lookupCategoryRepository) : base(context)
        {
            _context = context;
            _logger = logger;
            _lookupCategoryRepository = lookupCategoryRepository;
        }

        /// <summary>
        /// Search persons
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<SearchPersonResponse> SearchAsync(SearchPersonRequest request)
        {
            IQueryable<Person> query = _context.Persons
                // .Where(p => p.Id != request.UserId)
                .Include(c => c.Categories)
                .ThenInclude(d => d.Category)
                .OrderByDescending(p => p.DateCreated);

            if (request.Id.HasValue)
            {
                request.PageSize = 1;
                request.Page = 1;
                query = query.Where(p => p.Id == request.Id);
            }
            else
            {
                query = query.Where(q => q.Id != request.UserId);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                var nameParts = request.Name.Split(' ');

                query = query.Where(p =>
                    p.Firstname.ToLower().Contains(request.Name.ToLower()) ||
                    p.Lastname.ToLower().Contains(request.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.Category))
            {
                query = query.Where(c =>
                    c.Categories.Any(m
                        => m.Category.Name.ToLower() == request.Category.ToLower())
                );
            }

            var skip = (request.Page - 1) * request.PageSize;
            var hasMore = await query.Skip(skip).CountAsync() > 0;

            var people = await query
                .Include(s => s.Awards)
                .ThenInclude(a => a.Institute)
                .Include(s => s.Interests)
                .ThenInclude(i => i.Interest)
                .Include(s => s.Skills)
                .ThenInclude(s => s.Skill)
                .Include(s => s.Contacts)
                .ThenInclude(c => c.Contact)
                .Include(c => c.Projects)
                .Include(c => c.Employment)
                .Include(c => c.Stacks)
                .ThenInclude(c => c.Stack)
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync();

            people.ForEach(person =>
            {
                person.Connections = new List<PersonConnection>();
                person.FullName = $"{person.Firstname} {person.Lastname}";
                person.ConnectionsCount = _context.PersonConnections.Count(c => c.PersonId == person.Id);
                person.IsConnected =
                    _context.PersonConnections.Any(c => c.FollowerId == request.UserId && c.PersonId == person.Id);
            });

            return new SearchPersonResponse
            {
                Persons = people,
                Request = request,
                HasMore = hasMore
            };
        }

        #region Person Awards

        public async Task<IEnumerable<PersonAward>> GetAwardsAsync(Guid personId)
        {
            return await _context
                .PersonAwards
                .Where(p => p.PersonId == personId)
                .ToListAsync();
        }

        public async Task AddAwardAsync(PersonAward award)
        {
            await _context.PersonAwards.AddAsync(award);
            await _context.SaveChangesAsync(true);
        }

        public async Task UpdateAwardAsync(PersonAward award)
        {
            _context.PersonAwards.Update(award);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAwardAsync(Guid awardId, Guid personId)
        {
            var award = await _context
                .PersonAwards
                .FirstOrDefaultAsync(q =>
                    q.Id == awardId &&
                    q.PersonId == personId);

            _context.PersonAwards.Remove(award);

            await _context.SaveChangesAsync();
        }
        
        #endregion

        #region Tech Stack

        public async Task<IEnumerable<PersonStack>> GetStackAsync(Guid personId)
        {
            return await _context.DeveloperStack
                .Include(q => q.Stack)
                .Where(q => q.PersonId == personId)
                .ToListAsync();
        }

        public async Task AddStackAsync(PersonStack stack)
        {
            await _context.DeveloperStack.AddAsync(stack);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStackAsync(PersonStack stack)
        {
            _context.DeveloperStack.Update(stack);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStackAsync(Guid stackId, Guid personId)
        {
            var stack =
                await _context.DeveloperStack.FirstOrDefaultAsync(c =>
                    c.StackId == stackId && c.PersonId == personId);

            _context.DeveloperStack.Remove(stack);
            await _context.SaveChangesAsync();
        }

        #endregion
        
        #region Project

        public async Task<IEnumerable<PersonProject>> GetProjectsAsync(Guid personId)
        {
            return await _context.PersonProjects
                .Where(q => q.PersonId == personId)
                .ToListAsync();
        }

        public async Task AddProjectAsync(PersonProject stack)
        {
            await _context.PersonProjects.AddAsync(stack);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(PersonProject stack)
        {
            _context.PersonProjects.Update(stack);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(Guid projectId, Guid personId)
        {
            var project =
                await _context.PersonProjects.FirstOrDefaultAsync(c =>
                    c.Id == projectId && c.PersonId == personId);

            _context.PersonProjects.Remove(project);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Employment

        public async Task<IEnumerable<PersonEmployment>> GetEmploymentAsync(Guid personId)
        {
            return await _context.PersonEmploymentHistory
                .Where(q => q.PersonId == personId)
                .ToListAsync();
        }

        public async Task AddEmploymentAsync(PersonEmployment employment)
        {
            await _context.PersonEmploymentHistory.AddAsync(employment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmploymentAsync(PersonEmployment employment)
        {
            _context.PersonEmploymentHistory.Update(employment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmploymentAsync(Guid employmentId, Guid personId)
        {
            var employment =
                await _context.PersonEmploymentHistory.FirstOrDefaultAsync(c =>
                    c.Id == employmentId && c.PersonId == personId);

            _context.PersonEmploymentHistory.Remove(employment);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Category

        public async Task<IEnumerable<PersonCategory>> GetCategoriesAsync(Guid personId)
        {
            return await _context.PersonCategories
                .Include(p => p.Category)
                .Where(p => p.PersonId == personId)
                .ToListAsync();
        }

        public async Task<PersonCategory> AddCategoryAsync(PersonCategory category)
        {
            await _context.PersonCategories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task UpdateCategoryAsync(PersonCategory category)
        {
            _context.PersonCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Guid categoryId, Guid personId)
        {
            var category =
                await _context.PersonCategories.FirstOrDefaultAsync(c =>
                    c.CategoryId == categoryId && c.PersonId == personId);

            _context.PersonCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
        
        #endregion

        #region Interests

        public async Task<IEnumerable<PersonInterest>> GetInterestsAsync(Guid personId)
        {
            return await
                _context
                    .PersonInterests
                    .Include(p => p.Interest)
                    .Where(q => q.PersonId == personId)
                    .ToListAsync();
        }

        public async Task<PersonInterest> AddInterestAsync(PersonInterest interest)
        {
            await _context.PersonInterests.AddAsync(interest);
            await _context.SaveChangesAsync();

            return await _context.PersonInterests
                .Include(q => q.Interest)
                .SingleOrDefaultAsync(q =>
                    q.InterestId.Equals(interest.InterestId) && q.PersonId.Equals(interest.PersonId));
        }

        public async Task UpdateInterestAsync(PersonInterest interest)
        {
            _context.PersonInterests.Update(interest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInterestAsync(Guid interestId, Guid personId)
        {
            var interest =
                await _context.PersonInterests.FirstOrDefaultAsync(q =>
                    q.PersonId == personId && q.InterestId == interestId);

            _context.PersonInterests.Remove(interest);

            await _context.SaveChangesAsync();
        }

        #endregion

        #region Skills

        public async Task<IEnumerable<PersonSkill>> GetSkillsAsync(Guid personId)
        {
            return await _context.PersonSkills
                .Where(p => p.PersonId == personId)
                .ToListAsync();
        }

        public async Task<PersonSkill> AddSkillAsync(PersonSkill skill)
        {
            await _context.PersonSkills.AddAsync(skill);
            await _context.SaveChangesAsync();

            return await _context
                .PersonSkills
                .Include(q => q.Skill)
                .SingleOrDefaultAsync(q =>
                    q.PersonId == skill.PersonId &&
                    q.SkillId == skill.SkillId);
        }

        public async Task UpdateSkillAsync(PersonSkill skill)
        {
            _context.PersonSkills.Update(skill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSkillAsync(Guid skillId, Guid personId)
        {
            var entity =
                await _context.PersonSkills
                    .FirstOrDefaultAsync(s =>
                        s.SkillId == skillId &&
                        s.PersonId == personId);

            _context.PersonSkills.Remove(entity);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Connections

        public async Task<IEnumerable<PersonConnection>> GetConnectionsAsync(Guid personId)
        {
            return await _context.PersonConnections
                .Include(p => p.Person)
                .Where(p => p.FollowerId == personId)
                .Select(s => new PersonConnection
                {
                    Person = new Person
                    {
                        Firstname = s.Person.Firstname,
                        Lastname = s.Person.Lastname,
                        Avatar = s.Person.Avatar,
                        Id = s.Person.Id,
                        Bio = s.Person.Bio
                    },
                    Id = s.Id,
                    PersonId = s.PersonId,
                    FollowerId = s.FollowerId
                })
                .ToListAsync();
        }

        public async Task AddConnectionAsync(PersonConnection connection)
        {
            await _context.PersonConnections.AddAsync(connection);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateConnectionAsync(PersonConnection connection)
        {
            _context.PersonConnections.Update(connection);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteConnectionAsync(Guid connectionId)
        {
            var connection = await _context.PersonConnections
                .FirstOrDefaultAsync(q => q.Id == connectionId);

            _context.PersonConnections.Remove(connection);
            await _context.SaveChangesAsync();
        }

        #endregion

        public async Task<EmailSettings> EmailPreferences(Guid personId)
        {
            return await _context.EmailPreferences.SingleOrDefaultAsync(q => q.PersonId == personId);
        }
    
        #region Contacts

        public async Task<IEnumerable<Contact>> GetContactsAsync(Guid personId)
        {
            var contacts = await _context.Contacts
                .Where(q => q.BelongsTo == personId)
                .Where(q => !q.IsDeleted)
                .ToListAsync();

            return contacts;
        }

        public async Task AddContactAsync(PersonContact contact)
        {
            await _context.PersonContacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContactAsync(PersonContact contact)
        {
            _context.PersonContacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(Guid contactId, Guid belongsTo)
        {
            var contact =
                await _context.PersonContacts.FirstOrDefaultAsync(q =>
                    q.PersonId == belongsTo && q.ContactId == contactId);

            _context.PersonContacts.Remove(contact);

            await _context.SaveChangesAsync();
        }

        #endregion
    }
}