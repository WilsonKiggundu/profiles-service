using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Person;
using ProfileService.Models.Person;
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
        public async Task<SearchPersonResponse> SearchAsync(SearchPersonRequest request)
        {
            IQueryable<Person> query = _context.Persons.OrderByDescending(p => p.DateCreated);

            if (request.Id.HasValue)
            {
                request.PageSize = 1;
                request.Page = 1;
                query = query.Where(p => p.Id == request.Id);
            }
            
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(p => 
                    p.Firstname.ToLower().Contains(request.Name.ToLower()) || 
                    p.Lastname.ToLower().Contains(request.Name.ToLower()));
            }
            
            var skip = (request.Page - 1) * request.PageSize;
            var hasMore = await query.Skip(skip).CountAsync() > 0;

            var people = await query
                .Include(s => s.Awards)
                .ThenInclude(a => a.Institute)
                .Include(s => s.Categories)
                .ThenInclude(s => s.Category)
                .Include(s => s.Interests)
                .ThenInclude(i => i.Interest)
                .Include(s => s.Skills)
                .ThenInclude(s => s.Skill)
                .Include(s => s.Connections)
                // .ThenInclude(c => c.Person)
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync();
            
            people.ForEach(person =>
            {
                person.FullName = $"{person.Firstname} {person.Lastname}";
                person.ConnectionsCount = _context.PersonConnections.Count(c => c.PersonId == person.Id);
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

        public async Task DeleteAwardAsync(Guid awardId)
        {
            throw new NotImplementedException();
        }

        #endregion
        
        public async Task<IEnumerable<PersonCategory>> GetCategoriesAsync(Guid personId)
        {
            return await _context.PersonCategories
                .Include(p => p.Category)
                .Where(p => p.PersonId == personId)
                .ToListAsync();
        }

        public async Task AddCategoryAsync(PersonCategory category)
        {
            await _context.PersonCategories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(PersonCategory category)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCategoryAsync(Guid categoryId, Guid personId)
        {
            var category =
                await _context.PersonCategories.FirstOrDefaultAsync(c =>
                    c.CategoryId == categoryId && c.PersonId == personId);

            _context.PersonCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
    
        public async Task<IEnumerable<PersonInterest>> GetInterestsAsync(Guid personId)
        {
            return await 
                _context
                    .PersonInterests
                    .Include(p => p.Interest)
                    .Where(q => q.PersonId == personId)
                    .ToListAsync();
        }

        public async Task AddInterestAsync(PersonInterest interest)
        {
            await _context.PersonInterests.AddAsync(interest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInterestAsync(PersonInterest interest)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteInterestAsync(Guid interestId, Guid personId)
        {
            var interest =
                await _context.PersonInterests.FirstOrDefaultAsync(q =>
                    q.PersonId == personId && q.InterestId == interestId);

            _context.PersonInterests.Remove(interest);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PersonSkill>> GetSkillsAsync(Guid personId)
        {
            return await _context.PersonSkills
                .Where(p => p.PersonId == personId)
                .ToListAsync();
        }

        public async Task AddSkillAsync(PersonSkill skill)
        {
            await _context.PersonSkills.AddAsync(skill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSkillAsync(PersonSkill skill)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public async Task DeleteConnectionAsync(Guid connectionId)
        {
            throw new NotImplementedException();
        }
    }
}