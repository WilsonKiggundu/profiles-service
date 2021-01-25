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
        Task<SearchPersonResponse> SearchAsync(SearchPersonRequest request);
        
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
        Task DeleteCategoryAsync(Guid categoryId, Guid personId);            

        #endregion   
        
        #region Person Interests
    
        Task<IEnumerable<PersonInterest>> GetInterestsAsync(Guid personId);
        Task AddInterestAsync(PersonInterest interest);
        Task UpdateInterestAsync(PersonInterest interest);    
        Task DeleteInterestAsync(Guid interestId, Guid personId);                

        #endregion   
        
        #region Person Skills
    
        Task<IEnumerable<PersonSkill>> GetSkillsAsync(Guid personId);
        Task AddSkillAsync(PersonSkill skill);
        Task UpdateSkillAsync(PersonSkill skill);    
        Task DeleteSkillAsync(Guid skillId, Guid personId);            

        #endregion  
        
        #region Person Connections
    
        Task<IEnumerable<PersonConnection>> GetConnectionsAsync(Guid personId);
        Task AddConnectionAsync(PersonConnection connection);
        Task UpdateConnectionAsync(PersonConnection connection);    
        Task DeleteConnectionAsync(Guid connectionId);            

        #endregion  
    }
}