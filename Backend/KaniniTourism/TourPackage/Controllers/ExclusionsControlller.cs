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

    public class ExclusionsController : ControllerBase
    {
        private readonly IRepo<Exclusions, int> _exclusionsRepository;

        public ExclusionsController(IRepo<Exclusions, int> exclusionsRepository)
        {
            _exclusionsRepository = exclusionsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Exclusions>>> GetAllExclusions()
        {
            try
            {
                var exclusions = await _exclusionsRepository.GetAll();
                return Ok(exclusions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving exclusions.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exclusions>> GetExclusionById(int id)
        {
            try
            {
                var exclusion = await _exclusionsRepository.Get(id);
                if (exclusion == null)
                {
                    return NotFound();
                }

                return Ok(exclusion);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving exclusion.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Exclusions>> AddExclusion(Exclusions exclusion)
        {
            try
            {
                var addedExclusion = await _exclusionsRepository.Add(exclusion);
                if (addedExclusion != null)
                {
                    return Ok(addedExclusion);
                }
                return BadRequest("Cannot add exclusion now");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding exclusion.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Exclusions>> UpdateExclusion(Exclusions tourExclusion)
        {
            try
            {
              

                var existingExclusion = await _exclusionsRepository.Get(tourExclusion.ExclusionId);
                if (existingExclusion == null)
                {
                    return NotFound();
                }

                var updatedExclusion = await _exclusionsRepository.Update(tourExclusion);
                return Ok(updatedExclusion);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating exclusion.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Exclusions>> DeleteExclusion(int id)
        {
            try
            {
                var result = await _exclusionsRepository.Delete(id);
                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting exclusion.");
            }
        }
    }
}
