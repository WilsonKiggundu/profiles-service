using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Partner;
using ProfileService.Contracts.Partner.Address;
using ProfileService.Contracts.Partner.Contact;
using ProfileService.Contracts.Partner.Interest;
using ProfileService.Contracts.Partner.Contribution;
using ProfileService.Contracts.Partner.Portfolio;
using ProfileService.Contracts.Partner.Role;

namespace ProfileService.Services.Interfaces
{
    public interface IPartnerService : IService
    {
        Task<ICollection<GetPartner>> SearchAsync(SearchPartner request);
        Task<GetPartner> GetByIdAsync(Guid id);
        Task InsertAsync(NewPartner business);
        Task UpdateAsync(UpdatePartner business);
        Task DeleteAsync(Guid id);

        #region Partner Addresses
    
        Task<IEnumerable<GetPartnerAddress>> GetAddressesAsync(Guid businessId);
        Task AddAddressAsync(NewPartnerAddress address);
        Task UpdateAddressAsync(UpdatePartnerAddress address);    
        Task DeleteAddressAsync(Guid addressId);    

        #endregion    
        
        #region Partner Contacts
    
        Task<IEnumerable<GetPartnerContact>> GetContactsAsync(Guid businessId);
        Task AddContactAsync(NewPartnerContact contact);
        Task UpdateContactAsync(UpdatePartnerContact contact);        
        Task DeleteContactAsync(Guid contactId);    

        #endregion
        
        #region Partner Interests
    
        Task<IEnumerable<GetPartnerInterest>> GetInterestsAsync(Guid businessId);
        Task AddInterestAsync(NewPartnerInterest interest);
        Task UpdateInterestAsync(UpdatePartnerInterest interest);    
        Task DeleteInterestAsync(Guid interestId);        

        #endregion
        
        #region Partner Contributions
    
        Task<IEnumerable<GetPartnerContribution>> GetContributionsAsync(Guid businessId);
        Task AddContributionAsync(NewPartnerContribution need);    
        Task UpdateContributionAsync(UpdatePartnerContribution need);    
        Task DeleteContributionAsync(Guid needId);    

        #endregion
        
        #region Partner Portfolios
        
        Task<IEnumerable<GetPartnerPortfolio>> GetPortfoliosAsync(Guid businessId);
        Task AddPortfolioAsync(NewPartnerPortfolio product);
        Task UpdatePortfolioAsync(UpdatePartnerPortfolio product);    
        Task DeletePortfolioAsync(Guid productId);    

        #endregion
        
        #region Partner Roles
    
        Task<IEnumerable<GetPartnerRole>> GetRolesAsync(Guid businessId);
        Task AddRoleAsync(NewPartnerRole role);
        Task UpdateRoleAsync(UpdatePartnerRole role);        
        Task DeleteRoleAsync(Guid roleId);        

        #endregion
    }
}