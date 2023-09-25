using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginandSignup.Models
{
    public class Traveller
    {

        [Key]
        public int TravellerId { get; set; }
        [ForeignKey("TravellerId")]
        public User? Users { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string? Username { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid contact number format. Use a 10-digit number.")]
        public string? PhoneNumber { get; set; }

        public string? Gender { get; set; }


    }
}
