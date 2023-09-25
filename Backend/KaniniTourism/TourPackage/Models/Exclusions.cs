using System.ComponentModel.DataAnnotations;

namespace TourPackage.Models
{
    public class Exclusions
    {
        [Key]
        public int ExclusionId { get; set; }

        [Required]
        public int TravelAgentId { get; set; }

        [Required(ErrorMessage = "ExclusionDescription is required.")]
        [StringLength(500, ErrorMessage = "ExclusionDescription cannot exceed 500 characters.")]
        public string ExclusionDescription { get; set; }
    }
}
