using System;

namespace ProfileService.Contracts.Person
{
    /// <summary>
    /// Update person
    /// </summary>
    public class UpdatePerson
    {
        /// <summary>
        /// Person Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string Firstname { get; set; }
        
        /// <summary>
        /// Last name
        /// </summary>
        public string Lastname { get; set; }
    }
}