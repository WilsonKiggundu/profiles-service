using System.ComponentModel.DataAnnotations;

namespace ProfileService.Admin.Models.Common
{
    public class Skill : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}