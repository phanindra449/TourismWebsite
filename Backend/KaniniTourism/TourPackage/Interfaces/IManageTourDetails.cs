using TourPackage.Models;
using TourPackage.Models.DTO;

namespace TourPackage.Interfaces
{
    public interface IManageTourDetails
    {

        public Task<TourDetails?> UpdateBookedseats(UpdateBookedNoOfSeats tourdetails);

    }
}
