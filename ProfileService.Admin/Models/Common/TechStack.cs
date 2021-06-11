using System.ComponentModel.DataAnnotations;

namespace ProfileService.Admin.Models.Common
{
    public class TechStack : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}