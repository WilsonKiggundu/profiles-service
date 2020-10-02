using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Person;
using ProfileService.Models.Investor;
using ProfileService.Models.Person;

namespace ProfileService.Repositories.Interfaces
{
    /// <summary>
    /// Person repository
    /// </summary>
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<ICollection<Person>> SearchAsync(SearchPerson request);
        
        #region Person Awards
    
        Task<IEnumerable<PersonAward>> GetAwardsAsync(Guid personId);
        Task AddAwardAsync(PersonAward award);
        Task UpdateAwardAsync(PersonAward award);    
        Task DeleteAwardAsync(Guid awardId);            

        #endregion    
        
        #region Person Categories
    
        Task<IEnumerable<PersonCategory>> GetCategoriesAsync(Guid personId);
        Task AddCategoryAsync(PersonCategory category);
        Task UpdateCategoryAsync(PersonCategory category);    
        Task DeleteCategoryAsync(Guid categoryId);            

        #endregion   
        
        #region Person Interests
    
        Task<IEnumerable<PersonInterest>> GetInterestsAsync(Guid personId);
        Task AddInterestAsync(PersonInterest interest);
        Task UpdateInterestAsync(PersonInterest interest);    
        Task DeleteInterestAsync(Guid interestId);            

        #endregion   
        
        #region Person Skills
    
        Task<IEnumerable<PersonSkill>> GetSkillsAsync(Guid personId);
        Task AddSkillAsync(PersonSkill skill);
        Task UpdateSkillAsync(PersonSkill skill);    
        Task DeleteSkillAsync(Guid skillId);            

        #endregion   
    }
}