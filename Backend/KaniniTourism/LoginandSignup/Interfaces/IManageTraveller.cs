using LoginandSignup.Models;
using LoginandSignup.Models.DTO;

namespace LoginandSignup.Interfaces
{
    public interface IManageTraveller
    {

        public Task<UserDTO?> TravellerRegistration(TravellerDTO travellerDTO);


    }
}
