using System;

namespace ProfileService.Contracts.Person.Awards
{
    public class UpdatePersonAward    
    {    
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string AwardedBy { get; set; }
        public string Title { get; set; }    
        public string DateOfAward { get; set; }    
        public string Category { get; set; }
        public string Description { get; set; }
    }
}