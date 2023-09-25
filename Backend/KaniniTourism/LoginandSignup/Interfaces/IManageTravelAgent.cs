using LoginandSignup.Models.DTO;
using LoginandSignup.Models;
using LoginandRegistration.Models.DTO;

namespace LoginandSignup.Interfaces
{
    public interface IManageTravelAgent
    {

        public Task<UserDTO?> TravelAgentRegistration(TravelAgentDTO doctor);


        public Task<StatusDTO> StatusUpdate(StatusDTO status);

    }
}
