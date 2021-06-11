using System.ComponentModel.DataAnnotations;

namespace ProfileService.Admin.Models.Common
{
    public class Contribution : BaseModel
    {
        [Required]
        public string Category { get; set; }
    }
}