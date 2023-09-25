using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TourPackage.Models;

public class TourDate
{
    [Key]
    public int DateId { get; set; }

    [Required(ErrorMessage = "TourId is required.")]
    public int TourId { get; set; }

    [ForeignKey("TourId")]
    [JsonIgnore]
    public TourDetails? Tour { get; set; }

    [Required(ErrorMessage = "DepartureDate is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format for DepartureDate.")]
    public DateTime DepartureDate { get; set; }

    [Required(ErrorMessage = "ReturnDate is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format for ReturnDate.")]
    public DateTime ReturnDate { get; set; }
}
