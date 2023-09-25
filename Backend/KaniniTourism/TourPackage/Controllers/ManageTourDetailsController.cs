using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TourPackage.Exceptions;
using TourPackage.Interfaces;
using TourPackage.Models.DTO;
using TourPackage.Services;

namespace TourPackage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CORS")]

    public class ManageTourDetailsController : ControllerBase
    {
        private readonly IManageTourDetails _manageTourDetailsService;

        public ManageTourDetailsController(IManageTourDetails manageTourDetailsService)
        {
            _manageTourDetailsService = manageTourDetailsService;
        }

        [HttpPost("updateBookedSeats")]
        public async Task<IActionResult> UpdateBookedSeats([FromBody] UpdateBookedNoOfSeats updateBookedNoOfSeats)
        {
            try

            {
                var updatedTourDetails = await _manageTourDetailsService.UpdateBookedseats(updateBookedNoOfSeats);
                if (updatedTourDetails != null)
                {
                    // Return the updated tour details
                    return Ok(updatedTourDetails);
                }
                else
                {
                    // Return a bad request indicating insufficient availability
                    return BadRequest("Insufficient availability");
                }
            }
            catch (TourNotFoundException ex)
            {
                // Return a not found status with the exception message
                return NotFound(ex.Message);
            }
        }
    }
}
