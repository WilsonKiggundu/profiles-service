using AutoMapper;

namespace ProfileService.Contracts.Partner.Portfolio
{
    public class PartnerPortfolioMapper : Profile
    {
        public PartnerPortfolioMapper()
        {
            CreateMap<Models.Partner.PartnerPortfolio, GetPartnerPortfolio>();
            CreateMap<NewPartnerPortfolio, Models.Partner.PartnerPortfolio>()
                .ReverseMap();
            CreateMap<UpdatePartnerPortfolio, Models.Partner.PartnerPortfolio>()
                .ReverseMap();
        }
    }
}