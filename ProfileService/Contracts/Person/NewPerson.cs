using System;
using System.Collections.Generic;

namespace ProfileService.Contracts.Person
{
    /// <summary>
    /// New Person
    /// </summary>
    public class NewPerson
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Bio { get; set; }    
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string CoverPhoto { get; set; }
        
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }
}