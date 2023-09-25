using System.ComponentModel.DataAnnotations;

namespace TourPackage.Models
{
    public class Inclusions
    {

        [Key]
        public int InclusionId { get; set; }

        [Required]
        public int TravelAgentId { get; set; }


        [Required(ErrorMessage = "InclusionDescription is required.")]
        [StringLength(500, ErrorMessage = "InclusionDescription cannot exceed 500 characters.")]
        public string? InclusionDescriptionn { get; set; }
    }
}
