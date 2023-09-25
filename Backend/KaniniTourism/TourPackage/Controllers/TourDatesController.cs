using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourPackage.Models;
using TourPackage.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace TourPackage.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors("CORS")]

    public class TourDateController : ControllerBase
    {
        private readonly IRepo<TourDate, int> _tourDateRepository;

        public TourDateController(IRepo<TourDate, int> tourDateRepository)
        {
            _tourDateRepository = tourDateRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTourDates()
        {
            var tourDates = await _tourDateRepository.GetAll();
            return Ok(tourDates);
        }

        [HttpGet]
        public async Task<ICollection<TourDate>?> GetAll()
        {
            try
            {
                var tourDates = await _tourDateRepository.GetAll();
                if (tourDates.Count > 0)
                    return tourDates;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTourDateById(int id)
        {
            var tourDate = await _tourDateRepository.Get(id);
            if (tourDate == null)
            {
                return NotFound();
            }
            return Ok(tourDate);
        }

        [HttpPost]
        public async Task<IActionResult> AddTourDate(TourDate tourDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var addedTourDate = await _tourDateRepository.Add(tourDate);
            return CreatedAtAction(nameof(GetTourDateById), new { id = addedTourDate.DateId }, addedTourDate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTourDate(TourDate updatedTourDate)
        {


            var existingTourDate = await _tourDateRepository.Get(updatedTourDate.TourId);
            if (existingTourDate == null)
            {
                return NotFound();
            }
            var updatedTour = await _tourDateRepository.Update(updatedTourDate);

            if (updatedTour == null)
            {
                return NotFound();
            }

            return Ok(updatedTour);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourDate(int id)
        {
            var deletedTourDate = await _tourDateRepository.Delete(id);
            if (deletedTourDate == null)
            {
                return NotFound();
            }

            return Ok(deletedTourDate);
        }
    }
}
