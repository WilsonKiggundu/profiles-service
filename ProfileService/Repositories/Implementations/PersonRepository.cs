using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Person;
using ProfileService.Data;
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
        public async Task<ICollection<Person>> SearchAsync(SearchPerson request)
        {
            return await _context.Persons.ToListAsync();
        }

        #region Person Awards

        public async Task<IEnumerable<PersonAward>> GetAwardsAsync(Guid personId)
        {
            throw new NotImplementedException();
        }

        public async Task AddAwardAsync(PersonAward award)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAwardAsync(PersonAward award)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAwardAsync(Guid awardId)
        {
            throw new NotImplementedException();
        }

        #endregion
        

        public async Task<IEnumerable<PersonCategory>> GetCategoriesAsync(Guid personId)
        {
            throw new NotImplementedException();
        }

        public async Task AddCategoryAsync(PersonCategory category)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCategoryAsync(PersonCategory category)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PersonInterest>> GetInterestsAsync(Guid personId)
        {
            throw new NotImplementedException();
        }

        public async Task AddInterestAsync(PersonInterest interest)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateInterestAsync(PersonInterest interest)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteInterestAsync(Guid interestId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PersonSkill>> GetSkillsAsync(Guid personId)
        {
            throw new NotImplementedException();
        }

        public async Task AddSkillAsync(PersonSkill skill)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSkillAsync(PersonSkill skill)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteSkillAsync(Guid skillId)
        {
            throw new NotImplementedException();
        }
    }
}