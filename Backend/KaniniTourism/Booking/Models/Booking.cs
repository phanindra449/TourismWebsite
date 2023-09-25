using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingService.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int TourId { get; set; }

        public int UserId { get; set; }


        [Required(ErrorMessage = "Travel agent ID is required.")]
        public int TravelAgentId { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Contact email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? ContactEmail { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid contact number format. Use a 10-digit number.")]
        public string? ContactNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Number of participants must be at least 1.")]
        public int NumberOfParticipants { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Total price must be greater than 0.")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "Booking date is required.")]
        public DateTime BookingDate { get; set; }

        [StringLength(50, ErrorMessage = "Booking status cannot exceed 50 characters.")]
        public string? BookingStatus { get; set; }

        public string? SpecialRequests { get; set; }

        public ICollection<Passenger>? Passengers { get; set; }
    }
}
