using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TourPackage.Models;

public class TourExclusion
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "TourId is required.")]
    public int TourId { get; set; }

    [ForeignKey("TourId")]
    [JsonIgnore]
    public TourDetails? TourDetails { get; set; }

    [Required(ErrorMessage = "ExclusionId is required.")]
    public int ExclusionId { get; set; }

    [ForeignKey("ExclusionId")]
    [JsonIgnore]
    public Exclusions? Exclusions { get; set; }
}
