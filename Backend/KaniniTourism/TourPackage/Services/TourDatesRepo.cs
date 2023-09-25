using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TourPackage.Interfaces;
using TourPackage.Models;
using TourPackage.Models.Context;
using TourPackage.Models;

namespace TourPackage.Models
{
    public class TourDatesRepo : IRepo<TourDate,int>
    {
        private readonly TourContext _context;

        public TourDatesRepo(TourContext context)
        {
            _context = context;
        }
        public async Task<ICollection<TourDate?>> GetAll()
        {
            if (_context.TourDates != null)
            {

                var tourdetails = await _context.TourDates.ToListAsync();
                return tourdetails;

            }
            else
            {
                return null;
            }
        }


        public async Task< TourDate> Get(int id)
        {
            return _context.TourDates.FirstOrDefault(tourDate => tourDate.DateId == id);
        }

        public async Task<TourDate>Add(TourDate tourDate)
        {
            if (tourDate == null)
            {
                throw new ArgumentNullException(nameof(tourDate));
            }
            try 
            {

                _context.TourDates.Add(tourDate);
                await _context.SaveChangesAsync();
                return tourDate;
            }
           catch (Exception ex) { 
            
                throw new Exception( ex.Message);
            }
        }

        public async Task<TourDate> Update(TourDate tourDate)
        {
            if (tourDate == null)
            {
                throw new ArgumentNullException(nameof(tourDate));
            }

            var existingTourDate = _context.TourDates.FirstOrDefault(t => t.DateId == tourDate.DateId);
            if (existingTourDate != null)
            {
                existingTourDate.TourId = tourDate.TourId;
                existingTourDate.DepartureDate = tourDate.DepartureDate;
                existingTourDate.ReturnDate = tourDate.ReturnDate;
                return tourDate;
            }
            return null;
        }

        public async Task<TourDate> Delete(int id)
        {
            var tourDate = _context.TourDates.FirstOrDefault(t => t.DateId == id);
            if (tourDate != null)
            {
                _context.TourDates.Remove(tourDate);
                return tourDate;
            }
            return null;
        }
    }
}
