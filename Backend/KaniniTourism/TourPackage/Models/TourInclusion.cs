using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TourPackage.Models;

public class TourInclusion
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "TourId is required.")]
    public int TourId { get; set; }

    [ForeignKey("TourId")]
    [JsonIgnore]
    public TourDetails? Tour { get; set; }

    [Required(ErrorMessage = "InclusionId is required.")]
    public int InclusionId { get; set; }

    [ForeignKey("InclusionId")]
    [JsonIgnore]
    public Inclusions? Inclusions { get; set; }
}
