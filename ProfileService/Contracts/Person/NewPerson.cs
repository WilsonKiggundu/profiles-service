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
    }
}