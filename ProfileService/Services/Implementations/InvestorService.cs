using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Investor;
using ProfileService.Contracts.Investor.Address;
using ProfileService.Contracts.Investor.Contact;
using ProfileService.Contracts.Investor.Interest;
using ProfileService.Contracts.Investor.Portfolio;
using ProfileService.Models.Investor;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class InvestorService : IInvestorService
    {
        private readonly IInvestorRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public InvestorService(IInvestorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Search Investor
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ICollection<GetInvestor>> SearchAsync(SearchInvestor request)
        {
            var results = await _repository.SearchAsync(request);
            return _mapper.Map<ICollection<GetInvestor>>(results);
        }
        
        /// <summary>
        /// Get Investor by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetInvestor> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetInvestor>(result);
        }

        /// <summary>
        /// Insert Investor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task InsertAsync(NewInvestor model)
        {
            try
            {
                var business = _mapper.Map<Investor>(model);
                await _repository.InsertAsync(business);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// Update Investor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(UpdateInvestor model)
        {
            try
            {
                var business = _mapper.Map<Investor>(model);
                await _repository.InsertAsync(business);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// Delete Investor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetInvestorAddress>> GetAddressesAsync(Guid investorId)
        {
            var address = await _repository.GetAddressesAsync(investorId);
            return _mapper.Map<IEnumerable<GetInvestorAddress>>(address);
        }

        public async Task AddAddressAsync(NewInvestorAddress address)
        {
            try
            {
                var model = _mapper.Map<InvestorAddress>(address);
                await _repository.AddAddressAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAddressAsync(UpdateInvestorAddress address)
        {
            try
            {
                var model = _mapper.Map<InvestorAddress>(address);
                await _repository.UpdateAddressAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteAddressAsync(Guid addressId)
        {
            try
            {
                await _repository.DeleteAddressAsync(addressId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetInvestorContact>> GetContactsAsync(Guid investorId)
        {
            var contact = await _repository.GetContactsAsync(investorId);
            return _mapper.Map<IEnumerable<GetInvestorContact>>(contact);
        }

        public async Task AddContactAsync(NewInvestorContact contact)
        {
            try
            {
                var model = _mapper.Map<InvestorContact>(contact);
                await _repository.AddContactAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateContactAsync(UpdateInvestorContact contact)
        {
            try
            {
                var model = _mapper.Map<InvestorContact>(contact);
                await _repository.UpdateContactAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteContactAsync(Guid contactId)
        {
            try
            {
                await _repository.DeleteContactAsync(contactId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetInvestorInterest>> GetInterestsAsync(Guid investorId)
        {
            var interest = await _repository.GetInterestsAsync(investorId);
            return _mapper.Map<IEnumerable<GetInvestorInterest>>(interest);
        }

        public async Task AddInterestAsync(NewInvestorInterest interest)
        {
            try
            {
                var model = _mapper.Map<InvestorInterest>(interest);
                await _repository.AddInterestAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateInterestAsync(UpdateInvestorInterest interest)
        {
            try
            {
                var model = _mapper.Map<InvestorInterest>(interest);
                await _repository.UpdateInterestAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteInterestAsync(Guid interestId)
        {
            try
            {
                await _repository.DeleteInterestAsync(interestId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetInvestorPortfolio>> GetPortfoliosAsync(Guid investorId)
        {
            var portfolio = await _repository.GetPortfoliosAsync(investorId);
            return _mapper.Map<IEnumerable<GetInvestorPortfolio>>(portfolio);
        }

        public async Task AddPortfolioAsync(NewInvestorPortfolio portfolio)
        {
            try
            {
                var model = _mapper.Map<InvestorPortfolio>(portfolio);
                await _repository.AddPortfolioAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdatePortfolioAsync(UpdateInvestorPortfolio portfolio)
        {
            try
            {
                var model = _mapper.Map<InvestorPortfolio>(portfolio);
                await _repository.UpdatePortfolioAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeletePortfolioAsync(Guid portfolioId)
        {
            try
            {
                await _repository.DeletePortfolioAsync(portfolioId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}