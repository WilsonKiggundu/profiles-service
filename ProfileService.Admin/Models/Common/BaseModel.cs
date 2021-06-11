using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ProfileService.Admin.Models.Common
{
    /// <summary>
    /// Base model
    /// </summary>
    public abstract class BaseModel : IEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string DateCreated { get; set; } = DateTime.UtcNow.ToString("u");
        public string DateLastUpdated { get; set; } = DateTime.UtcNow.ToString("u");
    }
}