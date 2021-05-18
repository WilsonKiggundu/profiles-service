using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Person;
using ProfileService.Models.Business;
using ProfileService.Models.Common;
using ProfileService.Models.Investor;
using ProfileService.Models.Person;
using ProfileService.Models.Preferences;

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
        Task DeleteAwardAsync(Guid awardId, Guid personId);            

        #endregion    
        
        #region Person Stack
    
        Task<IEnumerable<PersonStack>> GetStackAsync(Guid personId);
        Task AddStackAsync(PersonStack stack);
        Task UpdateStackAsync(PersonStack stack);        
        Task DeleteStackAsync(Guid stackId, Guid personId);            

        #endregion 
        
        #region Person Project
    
        Task<IEnumerable<PersonProject>> GetProjectsAsync(Guid personId);
        Task AddProjectAsync(PersonProject stack);
        Task UpdateProjectAsync(PersonProject stack);            
        Task DeleteProjectAsync(Guid projectId, Guid personId);            

        #endregion  
        
        #region Person Terms
    
        Task<FreelanceTerms> GetFreelanceTermsAsync(Guid personId);
        Task AddFreelanceTermsAsync(FreelanceTerms terms);
        Task UpdateFreelanceTermsAsync(FreelanceTerms terms);            
        Task DeleteFreelanceTermsAsync(Guid personId);            

        #endregion  
        
        #region Person Employment
    
        Task<IEnumerable<PersonEmployment>> GetEmploymentAsync(Guid personId);
        Task AddEmploymentAsync(PersonEmployment stack);
        Task UpdateEmploymentAsync(PersonEmployment stack);                    
        Task DeleteEmploymentAsync(Guid projectId, Guid personId);            

        #endregion  
        
        #region Person Categories
    
        Task<IEnumerable<PersonCategory>> GetCategoriesAsync(Guid personId);
        Task<PersonCategory> AddCategoryAsync(PersonCategory category);
        Task UpdateCategoryAsync(PersonCategory category);    
        Task DeleteCategoryAsync(Guid categoryId, Guid personId);            

        #endregion   
        
        #region Person Interests
    
        Task<IEnumerable<PersonInterest>> GetInterestsAsync(Guid personId);
        Task<PersonInterest> AddInterestAsync(PersonInterest interest);
        Task UpdateInterestAsync(PersonInterest interest);    
        Task DeleteInterestAsync(Guid interestId, Guid personId);                

        #endregion   
        
        #region Person Skills
    
        Task<IEnumerable<PersonSkill>> GetSkillsAsync(Guid personId);
        Task<PersonSkill> AddSkillAsync(PersonSkill skill);
        Task UpdateSkillAsync(PersonSkill skill);    
        Task DeleteSkillAsync(Guid skillId, Guid personId);            

        #endregion  
        
        #region Person Connections
    
        Task<IEnumerable<PersonConnection>> GetConnectionsAsync(Guid personId);
        Task AddConnectionAsync(PersonConnection connection);
        Task UpdateConnectionAsync(PersonConnection connection);    
        Task DeleteConnectionAsync(Guid connectionId);            

        #endregion
        
        #region Business Contacts
    
        Task<IEnumerable<Contact>> GetContactsAsync(Guid businessId);
        Task AddContactAsync(PersonContact contact);
        Task UpdateContactAsync(PersonContact contact);        
        Task DeleteContactAsync(Guid contactId, Guid belongsTo);    

        #endregion

        #region Person Preferences

        Task<EmailSettings> EmailPreferences(Guid personId);

        #endregion
    }
}