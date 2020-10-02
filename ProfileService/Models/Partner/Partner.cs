using System.ComponentModel.DataAnnotations;
using ProfileService.Models.Common;

namespace ProfileService.Models.Partner
{
    public class Partner : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string IncorporationDate { get; set; }
        public string Website { get; set; }
    }
}