using System;
using ProfileService.Models.Common;

namespace ProfileService.Models.Person
{
    public class FreelanceTerms : BaseModel
    {
        public Guid PersonId { get; set; }
        
        /// <summary>
        /// Amount charged per RateType
        /// </summary>
        public string Rate { get; set; }
        
        /// <summary>
        /// Preferred currency type eg. UGX, USD, GBP
        /// </summary>
        public string Currency { get; set; }
        
        /// <summary>
        /// Rate type e.g. Hourly, Daily, Weekly, Monthly
        /// </summary>
        public RateType RateType { get; set; } = RateType.Hourly;
        
        // Anything else you want to say
        public string Details { get; set; }    
    }

    public enum RateType
    {
        Hourly = 1,
        Daily = 2,
        Weekly = 3,
        Monthly = 4,
        Quarterly = 5,
        Annually = 6,
        Other = 7
    }
}