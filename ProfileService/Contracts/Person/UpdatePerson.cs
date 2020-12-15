using System;
using System.Collections.Generic;

namespace ProfileService.Contracts.Person
{
    /// <summary>
    /// Update person
    /// </summary>
    public class UpdatePerson
    {    
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public string CoverPhoto { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string[] Interests { get; set; }
        public string[] Categories { get; set; }
    }
}