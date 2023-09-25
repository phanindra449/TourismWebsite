using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourPackage.Models
{
    public class TourDetails
    {
        [Key]
        public int TourId { get; set; }

        [Required(ErrorMessage = "Travel agent ID is required.")]
        public int TravelAgentId { get; set; }

        [Required(ErrorMessage = "Tour name is required.")]
        [StringLength(100, ErrorMessage = "Tour name cannot exceed 100 characters.")]
        public string TourName { get; set; }

        [Required(ErrorMessage = "Tour description is required.")]
        [StringLength(500, ErrorMessage = "Tour description cannot exceed 500 characters.")]
        public string TourDescription { get; set; }


        [Required(ErrorMessage = "Tour description is required.")]
        [StringLength(500, ErrorMessage = "Tour description cannot exceed 500 characters.")]
        public string TourTheme { get; set; }


        [Required(ErrorMessage = "Tour type is required.")]
        [StringLength(50, ErrorMessage = "Tour type cannot exceed 50 characters.")]
        public string Tourtype { get; set; }

        [Required(ErrorMessage = "Tour price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Tour price must be a non-negative value.")]
        public decimal TourPrice { get; set; }

        [Range(1, 30, ErrorMessage = "Number of days must be a positive integer.")]
        public int Noofdays { get; set; }

        [Required(ErrorMessage = "Maximum capacity is required.")]
        [Range(1, 100, ErrorMessage = "Maximum capacity must be a positive integer.")]
        public int MaxCapacity { get; set; }

        [Range(0, 100, ErrorMessage = "Booked capacity must be a non-negative integer.")]
        public int BookedNoOfSeats { get; set; }

        [Required(ErrorMessage = "Availability is required.")]
        public bool Availability { get; set; }

        [Required(ErrorMessage = "Tour Image is required.")]
        public string? TourImage { get; set; }  

        public ICollection<TourDestination?>? TourDestination { get; set; }
        public ICollection<TourDate?>? TourDate { get; set; }

        public ICollection<TourInclusion?>? TourInclusion { get; set; }
        public ICollection<TourExclusion?>? TourExclusion { get; set; }

    }
}
