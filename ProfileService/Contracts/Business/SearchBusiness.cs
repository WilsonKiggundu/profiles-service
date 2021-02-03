using System;
using System.Collections.Generic;

namespace ProfileService.Contracts.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchBusinessRequest
    {
        public Guid? PersonId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;    
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }

    public class SearchBusinessResponse
    {
        public SearchBusinessRequest Request { get; set; }
        public bool HasMore { get; set; }
        public IEnumerable<Models.Business.Business> Startups { get; set; }
    }
}