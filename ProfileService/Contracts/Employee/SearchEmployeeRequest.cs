using System;
using ProfileService.Models.Employees;

namespace ProfileService.Contracts.Employee
{
    public class SearchEmployeeRequest
    {
        public Guid? EmployeeId { get; set; }    
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public string Name { get; set; }
        public Department? Department { get; set; }    
        
        public Wellness? Status { get; set; }
    }
}