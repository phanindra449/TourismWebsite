using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TourPackage.Models;

public class TourDestination
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "TourId is required.")]
    public int TourId { get; set; }

    [ForeignKey("TourId")]
    [JsonIgnore]
    public TourDetails? Tour { get; set; }

    [Required(ErrorMessage = "DestinationId is required.")]
    public int DestinationId { get; set; }

    [ForeignKey("DestinationId")]
    [JsonIgnore]
    public Destination? Destination { get; set; }

    public string? DestinationImage { get; set; }

    public int DayNo { get; set; }


    [Required(ErrorMessage = "Event time is required.")]
    [DataType(DataType.Time)]
    public DateTime EventTime { get; set; }

    public string ActivityType { get; set; }
    public string DestinationActivity { get; set; }
    public string ActivityName { get; set; }

    
}
