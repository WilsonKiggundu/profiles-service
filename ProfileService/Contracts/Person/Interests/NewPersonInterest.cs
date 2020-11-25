using System;
using System.Runtime.InteropServices;

namespace ProfileService.Contracts.Person.Interests
{
    public class NewPersonInterest
    {
        public Guid PersonId { get; set; }    
        public Guid InterestId { get; set; }
    }
}