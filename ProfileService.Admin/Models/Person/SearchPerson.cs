using System;
using System.Collections.Generic;

namespace ProfileService.Admin.Models.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchPersonRequest
    {   
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public string Name { get; set; }

        public string Category { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class SearchPersonResponse
    {
        public SearchPersonRequest Request { get; set; }
        public bool HasMore { get; set; }
        public ICollection<Person> Persons { get; set; }
    }
}