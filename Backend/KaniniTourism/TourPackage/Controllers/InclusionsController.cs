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

    public class InclusionsController : ControllerBase
    {
        private readonly IRepo<Inclusions, int> _inclusionsRepository;

        public InclusionsController(IRepo<Inclusions, int> inclusionsRepository)
        {
            _inclusionsRepository = inclusionsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Inclusions>>> GetAllInclusions()
        {
            try
            {
                var inclusions = await _inclusionsRepository.GetAll();
                return Ok(inclusions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving inclusions.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inclusions>> GetInclusionById(int id)
        {
            try
            {
                var inclusion = await _inclusionsRepository.Get(id);
                if (inclusion == null)
                {
                    return NotFound();
                }

                return Ok(inclusion);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving inclusion.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Inclusions>> AddInclusion(Inclusions inclusion)
        {
            try
            {
                var addedInclusion = await _inclusionsRepository.Add(inclusion);
                if (addedInclusion != null)
                {
                    return Ok(addedInclusion);
                }
                return BadRequest("Cannot add inclusion now");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding inclusion.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Inclusions>> UpdateInclusion(Inclusions inclusion)
        {
            try
            {
                

                var existingInclusion = await _inclusionsRepository.Get(inclusion.InclusionId);
                if (existingInclusion == null)
                {
                    return NotFound();
                }

                var updatedInclusion = await _inclusionsRepository.Update(inclusion);
                return Ok(updatedInclusion);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating inclusion.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Inclusions>> DeleteInclusion(int id)
        {
            try
            {
                var result = await _inclusionsRepository.Delete(id);
                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting inclusion.");
            }
        }
    }
}
