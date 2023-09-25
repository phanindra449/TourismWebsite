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

    public class TourInclusionsController : ControllerBase
    {
        private readonly IRepo<TourInclusion, int> _tourInclusionsRepository;

        public TourInclusionsController(IRepo<TourInclusion, int> tourInclusionsRepository)
        {
            _tourInclusionsRepository = tourInclusionsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TourInclusion>>> GetAllTourInclusions()
        {
            try
            {
                var tourInclusions = await _tourInclusionsRepository.GetAll();
                return Ok(tourInclusions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving tour inclusions.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TourInclusion>> GetTourInclusionById(int id)
        {
            try
            {
                var tourInclusion = await _tourInclusionsRepository.Get(id);
                if (tourInclusion == null)
                {
                    return NotFound();
                }

                return Ok(tourInclusion);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving tour inclusion.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TourInclusion>> AddTourInclusion(TourInclusion tourInclusion)
        {
            try
            {
                var addedTourInclusion = await _tourInclusionsRepository.Add(tourInclusion);
                if (addedTourInclusion != null)
                {
                    return Ok(addedTourInclusion);
                }
                return BadRequest("Cannot add tour inclusion now");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding tour inclusion.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TourInclusion>> UpdateTourInclusion(TourInclusion tourInclusion)
        {
            try
            {
                

                var existingTourInclusion = await _tourInclusionsRepository.Get(tourInclusion.InclusionId);
                if (existingTourInclusion == null)
                {
                    return NotFound();
                }

                var updatedTourInclusion = await _tourInclusionsRepository.Update(tourInclusion);
                return Ok(updatedTourInclusion);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating tour inclusion.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TourInclusion>> DeleteTourInclusion(int id)
        {
            try
            {
                var result = await _tourInclusionsRepository.Delete(id);
                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting tour inclusion.");
            }
        }
    }
}
