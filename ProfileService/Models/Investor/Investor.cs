using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProfileService.Models.Common;

namespace ProfileService.Models.Investor
{
    public class Investor : BaseModel
    {
        [Required]
        public Guid PersonId { get; set; }
        public Person.Person Person { get; set; }

        [Required]
        public InvestorType Type { get; set; }

        public string InvestmentRange { get; set; }

        public string InvestmentStage { get; set; }    

        public string Website { get; set; }


        public bool IsVerified { get; set; } = false;
        
        [NotMapped] public bool IsProfileComplete { get; set; }

        public Investor()
        {
            IsProfileComplete = !string.IsNullOrEmpty(InvestmentRange)
                                && !string.IsNullOrEmpty(InvestmentStage);
        }
    }

    public enum InvestorType
    {
        AngleInvestor = 1,
        InstitutionalInvestor = 2,
        VentureCapitalist = 3,
        CorporateInvestor = 4,
        PeerToPeerInvestor = 5,
        Other = 6
    }
}