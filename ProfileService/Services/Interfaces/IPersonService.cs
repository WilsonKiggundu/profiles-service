using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Person;
using ProfileService.Contracts.Person.Awards;
using ProfileService.Contracts.Person.Categories;
using ProfileService.Contracts.Person.Connections;
using ProfileService.Contracts.Person.Interests;
using ProfileService.Contracts.Person.Skills;

namespace ProfileService.Services.Interfaces
{
    public interface IPersonService : IService
    {
        #region Person

        Task<SearchPersonResponse> SearchAsync(SearchPersonRequest request);
        Task<GetPerson> GetByIdAsync(Guid id);
        Task InsertAsync(NewPerson person);
        Task UpdateAsync(UpdatePerson person);
        Task UpdateCoverPhotoAsync(UpdatePerson person);
        Task DeleteAsync(Guid id);

        #endregion

        #region PersonAwards

        Task<IEnumerable<GetPersonAward>> GetAwardsAsync(Guid personId);
        Task AddAwardAsync(NewPersonAward award);
        Task UpdateAwardAsync(UpdatePersonAward award);    
        Task DeleteAwardAsync(Guid awardId);    

        #endregion
        
        #region PersonInterests

        Task<ICollection<GetPersonInterest>> GetInterestsAsync(Guid investorId);
        Task<NewPersonInterest> AddInterestAsync(NewPersonInterest model);    
        Task UpdateInterestAsync(UpdatePersonInterest award);    
        Task DeleteInterestAsync(Guid interestId, Guid personId);    

        #endregion
        
        #region PersonSkills

        Task<IEnumerable<GetPersonSkill>> GetSkillsAsync(Guid investorId);
        Task<NewPersonSkill> AddSkillAsync(NewPersonSkill award);
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
        Task AddCategoryAsync(NewPersonCategory category);
        Task UpdateCategoryAsync(UpdatePersonCategory category);    
        Task DeleteCategoryAsync(Guid categoryId, Guid personId);    

        #endregion

        Task UpdateAvatarAsync(UpdatePerson person);
        
    }
}