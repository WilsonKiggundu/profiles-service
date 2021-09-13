using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Business.Contact;
using ProfileService.Contracts.Lookup.Category;
using ProfileService.Contracts.Person;
using ProfileService.Contracts.Person.Awards;
using ProfileService.Contracts.Person.Categories;
using ProfileService.Contracts.Person.Connections;
using ProfileService.Contracts.Person.Contact;
using ProfileService.Contracts.Person.Interests;
using ProfileService.Contracts.Person.Skills;
using ProfileService.Models.Business;
using ProfileService.Models.Person;

namespace ProfileService.Services.Interfaces
{
    public interface IPersonService : IService
    {
        #region Person

        Task<int> CountAllAsync();

        Task<SearchPersonResponse> SearchAsync(SearchPersonRequest request);
        Task<GetPerson> GetByIdAsync(Guid id);
        Task<bool> GetProfileStatusAsync(Guid id);
        Task<NewPerson> InsertAsync(NewPerson person);
        Task<UpdatePerson> UpdateAsync(UpdatePerson person);
        Task<UpdatePerson> UpdateCoverPhotoAsync(UpdatePerson person);
        Task DeleteAsync(Guid id);

        #endregion

        #region PersonAwards

        Task<IEnumerable<GetPersonAward>> GetAwardsAsync(Guid personId);
        Task AddAwardAsync(NewPersonAward award);
        Task UpdateAwardAsync(UpdatePersonAward award);
        Task DeleteAwardAsync(Guid awardId, Guid personId);

        #endregion

        #region PersonInterests

        Task<ICollection<GetPersonInterest>> GetInterestsAsync(Guid investorId);
        Task<PersonInterest> AddInterestAsync(InterestViewModel interest, Guid personId);
        Task UpdateInterestAsync(UpdatePersonInterest award);
        Task DeleteInterestAsync(Guid interestId, Guid personId);

        #endregion
        
        #region Person Terms
    
        Task<FreelanceTerms> GetFreelanceTermsAsync(Guid personId);
        Task AddFreelanceTermsAsync(FreelanceTerms terms);
        Task UpdateFreelanceTermsAsync(FreelanceTerms terms);            
        Task DeleteFreelanceTermsAsync(Guid personId);            

        #endregion  

        #region PersonSkills

        Task<IEnumerable<GetPersonSkill>> GetSkillsAsync(Guid investorId);
        Task<PersonSkill> AddSkillAsync(SkillViewModel skill, Guid personId);
        Task UpdateSkillAsync(UpdatePersonSkill award);
        Task DeleteSkillAsync(Guid skillId, Guid personId);

        #endregion

        #region PersonConnections

        Task<IEnumerable<GetPersonConnection>> GetConnectionsAsync(Guid personId);
        Task<NewPersonConnection> AddConnectionAsync(NewPersonConnection connection);
        Task UpdateConnectionAsync(UpdatePersonConnection connection);
        Task DeleteConnectionAsync(Guid connectionId);

        #endregion

        #region PersonCategories

        Task<IEnumerable<GetPersonCategory>> GetCategoriesAsync(Guid investorId);
        Task<GetLookupCategory> AddCategoryAsync(CategoryViewModel category, Guid personId);
        Task UpdateCategoryAsync(UpdatePersonCategory category);
        Task DeleteCategoryAsync(Guid categoryId, Guid personId);

        #endregion

        Task<UpdatePerson> UpdateAvatarAsync(UpdatePerson person);
        Task<UpdatePerson> UpdateEmailAsync(UpdatePerson person);

        #region PersonContacts
    
        Task<IEnumerable<GetPersonContact>> GetContactsAsync(Guid personId);
        Task<PersonContact> AddContactAsync(NewPersonContact contact);
        Task<PersonContact> UpdateContactAsync(UpdatePersonContact contact);
        Task DeleteContactAsync(Guid contactId, Guid belongsTo);

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
        
        #region Person Employment
    
        Task<IEnumerable<PersonEmployment>> GetEmploymentAsync(Guid personId);
        Task AddEmploymentAsync(PersonEmployment stack);
        Task UpdateEmploymentAsync(PersonEmployment stack);                    
        Task DeleteEmploymentAsync(Guid projectId, Guid personId);            

        #endregion  
    }
}