using System;

namespace ProfileService.Contracts.Person
{
    /// <summary>
    /// New Person
    /// </summary>
    public class NewPerson
    {
        /// <summary>
        /// Person Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string Firstname { get; set; }
        
        /// <summary>
        /// Last name
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        
        /// <summary>
        /// Gender
        /// </summary>
        public Gender Gender { get; set; }    
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }
}