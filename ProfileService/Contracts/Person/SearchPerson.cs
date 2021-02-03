using System;
using System.Collections.Generic;

namespace ProfileService.Contracts.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchPersonRequest
    {   
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class SearchPersonResponse
    {
        public SearchPersonRequest Request { get; set; }
        public bool HasMore { get; set; }
        public ICollection<Models.Person.Person> Persons { get; set; }
    }
}