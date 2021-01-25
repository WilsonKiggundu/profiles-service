using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ProfileService.Contracts.Lookup.Interest;

namespace ProfileService.Contracts.Person.Interests
{
    public class NewPersonInterest
    {
        public Guid PersonId { get; set; }
        public List<InterestViewModel> Interests { get; set; }    
    }

    public class InterestViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}