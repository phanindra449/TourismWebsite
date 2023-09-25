using Microsoft.AspNetCore.Mvc;
using TourPackage.Models;
using TourPackage.Interfaces;
using  TourPackage.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace TourPackage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CORS")]

    public class TourDetailsController : ControllerBase
    {
        private readonly IRepo<TourDetails, int> _tourDetailsRepository;

        public TourDetailsController(IRepo<TourDetails ,int> tourDetailsRepository)
        {
            _tourDetailsRepository = tourDetailsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TourDetails>>> GetAllTourDetails()
        {
            try
            {
                var tourDetails = await _tourDetailsRepository.GetAll();
                return Ok(tourDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving tour details.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TourDetails>> GetTourDetailsById(int id)
        {
            try
            {
                var tourDetails = await _tourDetailsRepository.Get(id);
                if (tourDetails == null)
                {
                    return NotFound();
                }

                return Ok(tourDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving tour details.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TourDetails>> AddTourDetails(TourDetails tourDetails)
        {
            try
            {
                var addedTourDetails = await _tourDetailsRepository.Add(tourDetails);
                if (addedTourDetails != null)
                {
                    return Ok(addedTourDetails);
                }
                return BadRequest("Cannot add tourdetails now");         
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding tour details.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<TourDetails>> UpdateTourDetails(TourDetails tourDetails)
        {
            try
            {
               

                var existingTourDetails = await _tourDetailsRepository.Get(tourDetails.TourId);
                if (existingTourDetails == null)
                {
                    return NotFound();
                }

                var updatedTourDetails = await _tourDetailsRepository.Update(tourDetails);
                return Ok(updatedTourDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating tour details.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TourDetails>> DeleteTourDetails(int id)
        {
            try
            {
                var result = await _tourDetailsRepository.Delete(id);
                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting tour details.");
            }
        }
    }
}
