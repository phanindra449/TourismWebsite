using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginandSignup.Models
{
    public class TravelAgent
    {


        [Key]
        public int TravelAgentId { get; set; }
        [ForeignKey("TravelAgentId")]
        public User? Users { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string? Username { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Agency Name is required.")]
        [StringLength(100, ErrorMessage = "Agency Name cannot exceed 100 characters.")]
        public string? AgencyName { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid contact number format. Use a 10-digit number.")]
        public string? ContactNumber { get; set; }
            
        [Required(ErrorMessage = "Country is required.")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }

        [StringLength(100, ErrorMessage = "License Number cannot exceed 100 characters.")]
        public string? LicenseNumber { get; set; }

        public bool IsActive { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Years in Business must be a non-negative value.")]
        public int YearsInBusiness { get; set; }
        [Required(ErrorMessage = "License Expiry Date is required.")]
        [Display(Name = "License Expiry Date")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "License Expiry Date must be a future date.")]
        public DateTime LicenseExpiryDate { get; set; }

        public class FutureDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                DateTime date;
                if (DateTime.TryParse(value.ToString(), out date))
                {
                    return date >= DateTime.Today;
                }
                return false;
            }
        }

    }
}