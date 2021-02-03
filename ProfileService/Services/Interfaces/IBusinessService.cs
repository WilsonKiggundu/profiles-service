using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.Contracts.Business;
using ProfileService.Contracts.Business.Address;
using ProfileService.Contracts.Business.Contact;
using ProfileService.Contracts.Business.Interest;
using ProfileService.Contracts.Business.Need;
using ProfileService.Contracts.Business.Product;
using ProfileService.Contracts.Business.Role;
using ProfileService.Models.Business;

namespace ProfileService.Services.Interfaces
{
    public interface IBusinessService : IService
    {
        Task<SearchBusinessResponse> SearchAsync(SearchBusinessRequest request);
        Task<GetBusiness> GetByIdAsync(Guid id);
        Task<NewBusiness> InsertAsync(NewBusiness business);
        Task<UpdateBusiness> UpdateAsync(UpdateBusiness business);
        Task DeleteAsync(Guid id);

        #region Business Addresses
    
        Task<IEnumerable<GetBusinessAddress>> GetAddressesAsync(Guid businessId);
        Task<NewBusinessAddress> AddAddressAsync(NewBusinessAddress address);
        Task UpdateAddressAsync(UpdateBusinessAddress address);    
        Task DeleteAddressAsync(Guid businessId, Guid addressId);    

        #endregion    
        
        #region Business Contacts
    
        Task<IEnumerable<GetBusinessContact>> GetContactsAsync(Guid businessId);
        Task<BusinessContact> AddContactAsync(NewBusinessContact contact);
        Task<BusinessContact> UpdateContactAsync(UpdateBusinessContact contact);        
        Task DeleteContactAsync(Guid contactId, Guid belongsTo);    

        #endregion
        
        #region Business Interests
    
        Task<IEnumerable<GetBusinessInterest>> GetInterestsAsync(Guid businessId);
        Task<GetBusinessInterest> AddInterestAsync(NewBusinessInterest interest, Guid businessId);
        Task UpdateInterestAsync(UpdateBusinessInterest interest);    
        Task DeleteInterestAsync(Guid businessId, Guid interestId);        

        #endregion
        
        #region Business Needs
    
        Task<IEnumerable<GetBusinessNeed>> GetNeedsAsync(Guid businessId);
        Task AddNeedAsync(NewBusinessNeed need);    
        Task UpdateNeedAsync(UpdateBusinessNeed need);    
        Task DeleteNeedAsync(Guid needId);    

        #endregion
        
        #region Business Products
        
        Task<IEnumerable<GetBusinessProduct>> GetProductsAsync(Guid businessId);
        Task<GetBusinessProduct> AddProductAsync(NewBusinessProduct product);
        Task<GetBusinessProduct> UpdateProductAsync(UpdateBusinessProduct product);    
        Task DeleteProductAsync(Guid productId, Guid businessId);    

        #endregion
        
        #region Business Roles
    
        Task<IEnumerable<GetBusinessRole>> GetRolesAsync(Guid businessId);
        Task<BusinessRole> AddRoleAsync(NewBusinessRole role);
        Task UpdateRoleAsync(UpdateBusinessRole role);        
        Task DeleteRoleAsync(Guid businessId, Guid personId);        

        #endregion

        Task<UpdateBusiness> UpdateCoverPhotoAsync(UpdateBusiness business);
        Task<UpdateBusiness> UpdateAvatarAsync(UpdateBusiness business);
    }
}