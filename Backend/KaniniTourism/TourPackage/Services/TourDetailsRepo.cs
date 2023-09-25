using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourPackage.Models.Context;
using TourPackage.Interfaces;
using TourPackage.Exceptions;
using System.Diagnostics;

namespace TourPackage.Models
{
    public class TourDetailsRepo : IRepo<TourDetails,int>
    {
        private readonly TourContext _context;

        public TourDetailsRepo(TourContext context)
        {
            _context = context;
        }

        public async Task<ICollection<TourDetails?>> GetAll()
        {
            if (_context.TourDetails != null)
            {

                var tourdetails= await _context.TourDetails.Include(u=>u.TourInclusion).Include(s=>s.TourExclusion).Include(t=>t.TourDate).Include(d=>d.TourDestination).ToListAsync();
                return tourdetails;
                
            }
            else
            {
                return null;
            }
        }



        public async Task<TourDetails?> Get(int id)
        {

            if (_context.TourDetails != null)
            {
                var tourdetails = _context.TourDetails.Include(u => u.TourInclusion).Include(s => s.TourExclusion).Include(t => t.TourDate).Include(d => d.TourDestination).FirstOrDefault(u => u.TourId == id);
                if (tourdetails != null)
                {
                    return tourdetails;

                }
                else
                {
                    throw new TourDetailsNotFoundException("TourDetails not found");


                }
            }
            else
            {
                throw new DatabaseEmptyException("Database is empty");
            }
        }
      
        public async Task<TourDetails> Add(TourDetails tourDetails)
        {
            _context.TourDetails.Add(tourDetails);
            await _context.SaveChangesAsync();
            return tourDetails;
        }

        public async Task<TourDetails> Update(TourDetails updatedtourDetails)
        {

            var tourdetails =await  Get(updatedtourDetails.TourId);
            if (tourdetails != null)
            {
                tourdetails.TourName = updatedtourDetails.TourName;
                tourdetails.TourDescription= updatedtourDetails.TourDescription;
                tourdetails.TourPrice = updatedtourDetails.TourPrice;
                tourdetails.MaxCapacity = updatedtourDetails.MaxCapacity;
                tourdetails.BookedNoOfSeats = updatedtourDetails.BookedNoOfSeats;
                tourdetails.Availability = updatedtourDetails.Availability;  
                tourdetails.Noofdays = updatedtourDetails.Noofdays;
            }
            else
            {
                throw new TourDetailsNotFoundException("Tour Details not found");
            }

            await _context.SaveChangesAsync();
            Debug.WriteLine(tourdetails);
            return tourdetails;
        }

        public async Task<TourDetails> Delete(int id)
        {
            var tourDetails = await _context.TourDetails.FirstOrDefaultAsync(u=>u.TourId==id);
            if (tourDetails == null)
            {
                throw new Exception("There is no data with particular tour Id");
            }

            _context.TourDetails.Remove(tourDetails);
            await _context.SaveChangesAsync();
            return tourDetails;
        }


    
    }
}
