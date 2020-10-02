using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Partner;
using ProfileService.Models.Partner;

namespace ProfileService.Repositories.Interfaces
{
    /// <summary>
    /// Partner repository
    /// </summary>
    public interface IPartnerRepository : IGenericRepository<Partner>
    {
        /// <summary>
        /// Search person
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ICollection<Partner>> SearchAsync(SearchPartner request);

        #region Partner Addresses
    
        Task<IEnumerable<PartnerAddress>> GetAddressesAsync(Guid partnerId);
        Task AddAddressAsync(PartnerAddress address);
        Task UpdateAddressAsync(PartnerAddress address);    
        Task DeleteAddressAsync(Guid addressId);    

        #endregion    
        
        #region Partner Contacts
    
        Task<IEnumerable<PartnerContact>> GetContactsAsync(Guid partnerId);
        Task AddContactAsync(PartnerContact contact);
        Task UpdateContactAsync(PartnerContact contact);    
        Task DeleteContactAsync(Guid contactId);    

        #endregion
        
        #region Partner Interests
    
        Task<IEnumerable<PartnerInterest>> GetInterestsAsync(Guid partnerId);
        Task AddInterestAsync(PartnerInterest interest);
        Task UpdateInterestAsync(PartnerInterest interest);    
        Task DeleteInterestAsync(Guid interestId);    

        #endregion

        #region Partner Products
        
        Task<IEnumerable<PartnerPortfolio>> GetPortfoliosAsync(Guid partnerId);
        Task AddPortfolioAsync(PartnerPortfolio portfolio);    
        Task UpdatePortfolioAsync(PartnerPortfolio portfolio);    
        Task DeletePortfolioAsync(Guid productId);    

        #endregion
        
        #region Partner Roles
    
        Task<IEnumerable<PartnerRole>> GetRolesAsync(Guid partnerId);
        Task AddRoleAsync(PartnerRole role);
        Task UpdateRoleAsync(PartnerRole role);    
        Task DeleteRoleAsync(Guid roleId);        

        #endregion
        
        #region Partner Contribution
    
        Task<IEnumerable<PartnerContribution>> GetContributionsAsync(Guid partnerId);
        Task AddContributionAsync(PartnerContribution role);
        Task UpdateContributionAsync(PartnerContribution role);    
        Task DeleteContributionAsync(Guid roleId);        

        #endregion

    }
}