using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models.Common
{
    /// <summary>
    /// User interests
    /// </summary>
    public class Interest : BaseModel
    {
        [Required]
        public string Category { get; set; }
        public Guid? IconId { get; set; }
        public Upload Icon { get; set; }
    }
}