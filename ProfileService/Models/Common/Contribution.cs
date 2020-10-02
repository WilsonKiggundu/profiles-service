using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models.Common
{
    public class Contribution : BaseModel
    {
        [Required]
        public string Category { get; set; }
    }
}