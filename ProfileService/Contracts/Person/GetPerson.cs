using System;

namespace ProfileService.Contracts.Person
{
    /// <summary>
    /// Get a person
    /// </summary>
    public class GetPerson
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
        /// Profile photo
        /// </summary>
        public string Avatar { get; set; }
    }
}