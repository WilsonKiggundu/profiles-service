using System;
using System.Collections.Generic;
using ProfileService.Contracts.Lookup.Category;
using ProfileService.Contracts.Lookup.Interest;
using ProfileService.Contracts.Person.Awards;
using ProfileService.Contracts.Person.Skills;

namespace ProfileService.Contracts.Person
{
    /// <summary>
    /// Get a person
    /// </summary>
    public class GetPerson
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Bio { get; set; }
        public string DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public string CoverPhoto { get; set; }
        public string Gender { get; set; }

        public ICollection<GetLookupCategory> Categories { get; set; }
        public ICollection<GetLookupInterest> Interests { get; set; }    
        public ICollection<GetPersonAward> Awards { get; set; }    
        public ICollection<GetPersonSkill> Skills { get; set; }    
    }
}