using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

public class Destination
{
    [Key]
    public int DestinationId { get; set; }

    [Required]
    public int TravelAgentId { get; set; }
    [Required(ErrorMessage = "DestinationName is required.")]
    [StringLength(100, ErrorMessage = "DestinationName cannot exceed 100 characters.")]

    public string DestinationName { get; set; }

    [Required(ErrorMessage = "Country is required.")]
    [StringLength(30, ErrorMessage = "Country cannot exceed 100 characters.")]
    public string Country { get; set; }

    [Required(ErrorMessage = "City is required.")]
    [StringLength(30, ErrorMessage = "City cannot exceed 100 characters.")]
    public string City { get; set; }

    [Required(ErrorMessage = "SpotDescription is required.")]
    [StringLength(300, ErrorMessage = "City cannot exceed 100 characters.")]

    public string SpotDescription { get; set; }
}
