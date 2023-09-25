using Microsoft.EntityFrameworkCore.Migrations.Operations;
using TourPackage.Interfaces;
using TourPackage.Models;
using TourPackage.Models.DTO;
using TourPackage.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace TourPackage.Services
{
    public class ManageTourDetailsService :IManageTourDetails
    {

        private readonly IRepo<TourDetails, int> _tourdetailsRepo;
        public ManageTourDetailsService(IRepo<TourDetails , int> tourdetailsRepo)
        {
            _tourdetailsRepo = tourdetailsRepo;
        }

        public async Task<TourDetails?> UpdateBookedseats(UpdateBookedNoOfSeats updateBookedNoOfSeats)
        {

            
            var tourdetails = await _tourdetailsRepo.Get(updateBookedNoOfSeats.TourId);
           
            if (tourdetails != null)
            {
                if (tourdetails.MaxCapacity < tourdetails.BookedNoOfSeats + updateBookedNoOfSeats.TravellerBookedseats)
                {
                    return null;
                }
                else
                {
                    tourdetails.BookedNoOfSeats = tourdetails.BookedNoOfSeats + updateBookedNoOfSeats.TravellerBookedseats;
                    if (tourdetails.BookedNoOfSeats == tourdetails.MaxCapacity)
                    {
                        tourdetails.Availability = false;
                    }
                    var vsdgd = await _tourdetailsRepo.Update(tourdetails);
                    return vsdgd;
                }
            }
            else
            {
                throw new TourNotFoundException("Tour Details Not Found");
            }


        }
    }
}
