using System;
using System.Text.Json.Serialization;

namespace ProfileService.Admin.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public string DateLastUpdated { get; set; }  
    }
}