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

    public class DestinationsController : ControllerBase
    {
        private readonly IRepo<Destination, int> _destinationsRepository;

        public DestinationsController(IRepo<Destination, int> destinationsRepository)
        {
            _destinationsRepository = destinationsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Destination>>> GetAllDestinations()
        {
            try
            {
                var destinations = await _destinationsRepository.GetAll();
                return Ok(destinations);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving destinations.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Destination>> GetDestinationById(int id)
        {
            try
            {
                var destination = await _destinationsRepository.Get(id);
                if (destination == null)
                {
                    return NotFound();
                }

                return Ok(destination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving destination.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Destination>> AddDestination(Destination destination)
        {
            try
            {
                var addedDestination = await _destinationsRepository.Add(destination);
                if (addedDestination != null)
                {
                    return Ok(addedDestination);
                }
                return BadRequest("Cannot add destination now");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding destination.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Destination>> UpdateDestination(Destination destination)
        {
            try
            {
                

                var existingDestination = await _destinationsRepository.Get(destination.DestinationId);
                if (existingDestination == null)
                {
                    return NotFound();
                }

                var updatedDestination = await _destinationsRepository.Update(destination);
                return Ok(updatedDestination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating destination.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Destination>> DeleteDestination(int id)
        {
            try
            {
                var result = await _destinationsRepository.Delete(id);
                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting destination.");
            }
        }
    }
}
