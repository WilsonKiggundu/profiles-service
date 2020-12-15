using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        /// <param name="exclude"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ICollection<Person>> SearchAsync(Guid? exclude)
        {
            return await _context.Persons
                //.Where(p => p.Id != exclude)
                .ToListAsync();
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

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            throw new NotImplementedException();
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

        public async Task DeleteInterestAsync(Guid interestId)
        {
            throw new NotImplementedException();
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

        public async Task DeleteSkillAsync(Guid skillId)
        {
            throw new NotImplementedException();
        }
    }
}