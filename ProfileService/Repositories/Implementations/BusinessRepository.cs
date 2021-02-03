using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Contracts.Business;
using ProfileService.Models.Business;
using ProfileService.Models.Common;
using ProfileService.Repositories.Interfaces;

namespace ProfileService.Repositories.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessRepository : GenericRepository<Business>, IBusinessRepository
    {
        private readonly ProfileServiceContext _context;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BusinessRepository(ProfileServiceContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Search Business
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<SearchBusinessResponse> SearchAsync(SearchBusinessRequest request)
        {
            IQueryable<Business> query = _context.Businesses.OrderByDescending(p => p.DateCreated);

            if (request.Id.HasValue)
            {
                request.PageSize = 1;
                request.Page = 1;
                query = query.Where(p => p.Id == request.Id);
            }

            if (request.PersonId.HasValue)
            {
                query = query
                    .Include(r => r.Roles)
                    .Where(p => p.Roles.Any(r => r.PersonId == request.PersonId));
            }
            
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(request.Name.ToLower()));
            }
            
            var skip = (request.Page - 1) * request.PageSize;
            var hasMore = await query.Skip(skip).CountAsync() > 0;

            var startups = await query
                .Include(s => s.Addresses)
                .Include(s => s.Roles)
                .ThenInclude(r => r.Person)
                .Include(s => s.Contacts)
                .ThenInclude(c => c.Contact)
                .Include(s => s.Interests)
                .ThenInclude(i => i.Interest)
                .Include(s => s.Products)
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync();
            
            return new SearchBusinessResponse
            {
                Startups = startups,
                Request = request,
                HasMore = hasMore
            };
        }

        public async Task<IEnumerable<BusinessAddress>> GetAddressesAsync(Guid businessId)
        {
            return await _context.BusinessAddresses
                .Where(q => q.BusinessId == businessId)
                .ToListAsync();
        }

        public async Task AddAddressAsync(BusinessAddress address)
        {
            await _context.BusinessAddresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(BusinessAddress address)
        {
            _context.BusinessAddresses.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(Guid businessId, Guid addressId)
        {
            var address = await _context.BusinessAddresses
                .FirstAsync(q => q.Id == addressId && q.BusinessId == businessId);
            _context.BusinessAddresses.Remove(address);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync(Guid businessId)
        {
            var contacts = await _context.Contacts
                .Where(q => q.BelongsTo == businessId)
                .Where(q => !q.IsDeleted)
                .ToListAsync();

            return contacts;
        }

        public async Task AddContactAsync(BusinessContact contact)
        {
            await _context.BusinessContacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContactAsync(BusinessContact contact)
        {
            _context.BusinessContacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(Guid contactId, Guid belongsTo)
        {
            var contact =
                await _context.BusinessContacts.FirstOrDefaultAsync(q =>
                    q.BusinessId == belongsTo && q.ContactId == contactId);

            _context.BusinessContacts.Remove(contact);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BusinessInterest>> GetInterestsAsync(Guid businessId)
        {
            return await _context.BusinessInterests
                .Include(p => p.Interest)
                .Where(q => q.BusinessId == businessId)
                .Where(q => !q.IsDeleted)
                .ToListAsync();
        }

        public async Task AddInterestAsync(BusinessInterest interest)
        {
            await _context.BusinessInterests
                .AddAsync(interest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInterestAsync(BusinessInterest interest)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteInterestAsync(Guid businessId, Guid interestId)
        {
            var interest = await _context.BusinessInterests
                .FirstOrDefaultAsync(q =>
                q.BusinessId.Equals(businessId) && q.InterestId.Equals(interestId));
            
            _context.BusinessInterests.Remove(interest);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BusinessNeed>> GetNeedsAsync(Guid businessId)
        {
            throw new NotImplementedException();
        }

        public async Task AddNeedAsync(BusinessNeed need)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateNeedAsync(BusinessNeed need)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteNeedAsync(Guid needId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BusinessProduct>> GetProductsAsync(Guid businessId)
        {
            return await _context.BusinessProducts.Where(q => q.BusinessId == businessId).ToListAsync();
        }

        public async Task AddProductAsync(BusinessProduct product)
        {
            await _context.BusinessProducts.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<BusinessProduct> UpdateProductAsync(BusinessProduct product)
        {
            _context.BusinessProducts.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(Guid productId, Guid businessId)
        {
            var product =
                await _context.BusinessProducts.FirstOrDefaultAsync(
                    p => p.BusinessId == businessId && p.Id == productId);

            _context.BusinessProducts.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BusinessRole>> GetRolesAsync(Guid businessId)
        {
            return await _context.BusinessRoles.Include(r => r.Person).Where(q => q.BusinessId == businessId).ToListAsync();
        }

        public async Task AddRoleAsync(BusinessRole role)
        {
            await _context.BusinessRoles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(BusinessRole role)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRoleAsync(Guid businessId, Guid personId)
        {
            var role = await _context.BusinessRoles.FirstOrDefaultAsync(f =>
                f.BusinessId == businessId && f.PersonId == personId);

            _context.BusinessRoles.Remove(role);

            await _context.SaveChangesAsync();
        }
    }
}