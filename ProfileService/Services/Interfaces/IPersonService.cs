using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Person;
using ProfileService.Contracts.Person.Awards;
using ProfileService.Contracts.Person.Categories;
using ProfileService.Contracts.Person.Interests;
using ProfileService.Contracts.Person.Skills;

namespace ProfileService.Services.Interfaces
{
    public interface IPersonService : IService
    {
        #region Person

        Task<ICollection<GetPerson>> SearchAsync(Guid? exclude);
        Task<GetPerson> GetByIdAsync(Guid id);
        Task InsertAsync(NewPerson person);
        Task UpdateAsync(UpdatePerson person);
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
        Task AddInterestAsync(NewPersonInterest award);
        Task UpdateInterestAsync(UpdatePersonInterest award);    
        Task DeleteInterestAsync(Guid awardId);    

        #endregion
        
        #region PersonSkills

        Task<IEnumerable<GetPersonSkill>> GetSkillsAsync(Guid investorId);
        Task AddSkillAsync(NewPersonSkill award);
        Task UpdateSkillAsync(UpdatePersonSkill award);    
        Task DeleteSkillAsync(Guid awardId);    

        #endregion
        
        #region PersonCategories

        Task<IEnumerable<GetPersonCategory>> GetCategoriesAsync(Guid investorId);
        Task AddCategoryAsync(NewPersonCategory award);
        Task UpdateCategoryAsync(UpdatePersonCategory award);    
        Task DeleteCategoryAsync(Guid awardId);    

        #endregion
        
    }
}