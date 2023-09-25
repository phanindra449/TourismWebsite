using System.ComponentModel.DataAnnotations;

namespace LoginandRegistration.Models.DTO
{
    public class StatusDTO
    {


        [Required]
        public int TravelAgentId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    
}
}
