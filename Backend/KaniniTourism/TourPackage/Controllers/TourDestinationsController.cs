using Microsoft.AspNetCore.Mvc;
using TourPackage.Models;
using TourPackage.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TourPackage.Exceptions;
using System;
using Microsoft.AspNetCore.Cors;

namespace TourPackage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CORS")]

    public class TourDestinationsController : ControllerBase
    {
        private readonly IRepo<TourDestination, int> _tourDestinationsRepository;

        public TourDestinationsController(IRepo<TourDestination, int> tourDestinationsRepository)
        {
            _tourDestinationsRepository = tourDestinationsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TourDestination>>> GetAllTourDestinations()
        {
            try
            {
                var tourDestinations = await _tourDestinationsRepository.GetAll();
                return Ok(tourDestinations);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving tour destinations.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TourDestination>> GetTourDestinationById(int id)
        {
            try
            {
                var tourDestination = await _tourDestinationsRepository.Get(id);
                if (tourDestination == null)
                {
                    return NotFound();
                }

                return Ok(tourDestination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving tour destination.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TourDestination>> AddTourDestination(TourDestination tourDestination)
        {
            try
            {
                var addedTourDestination = await _tourDestinationsRepository.Add(tourDestination);
                if (addedTourDestination != null)
                {
                    return Ok(addedTourDestination);
                }
                return BadRequest("Cannot add tour destination now");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding tour destination.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<TourDestination>> UpdateTourDestination(TourDestination tourDestination)
        {
            try
            {

                var existingTourDestination = await _tourDestinationsRepository.Get(tourDestination.DestinationId);
                if (existingTourDestination == null)
                {
                    return NotFound();
                }

                var updatedTourDestination = await _tourDestinationsRepository.Update(tourDestination);
                return Ok(updatedTourDestination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating tour destination.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TourDestination>> DeleteTourDestination(int id)
        {
            try
            {
                var result = await _tourDestinationsRepository.Delete(id);
                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting tour destination.");
            }
        }
    }
}
