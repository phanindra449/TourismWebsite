
using LoginandSignup.Interfaces;
using LoginandSignup.Models.DTO;
using LoginandSignup.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using LoginandRegistration.Models.DTO;

namespace LoginandRegistration.Services
{
    public class ManageTravelAgentService : IManageTravelAgent
    {
        private readonly IRepo<TravelAgent, int> _travelagentRepo;
        private readonly IRepo<User, int> _userRepo;
        private readonly IGenerateToken _tokenService;

        public ManageTravelAgentService(IRepo<TravelAgent, int> travelagentRepo, IRepo<User, int> userRepo, IGenerateToken tokenService)
        {

            _travelagentRepo = travelagentRepo;
            _userRepo = userRepo;
            _tokenService = tokenService;
        }

        public async Task<UserDTO> TravelAgentRegistration(TravelAgentDTO user)
        {
            UserDTO myUser = null;
            var hmac = new HMACSHA512();
            user.Users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordClear));
            user.Users.PasswordKey = hmac.Key;
            user.Users.Role = "TravelAgent";
            user.IsActive = false;

            var users = await _travelagentRepo.GetAll();
            if (users != null)
            {
                var myAdminUser = users.FirstOrDefault(u => u.ContactNumber == user.ContactNumber && u.Username == user.Username);
                if (myAdminUser != null)
                {
                    return null;
                }
            }
            var userResult = await _userRepo.Add(user.Users);
            var travelagentResult = await _travelagentRepo.Add(user);
            if (userResult != null && travelagentResult != null)
            {
                myUser = new UserDTO();
                myUser.UserId = travelagentResult.TravelAgentId;
                myUser.Role = userResult.Role;
                myUser.Token = _tokenService.GenerateToken(myUser);
            }
            return myUser;
        }

        public async Task<StatusDTO> StatusUpdate(StatusDTO status)
        {
            var travelagent = await _travelagentRepo.Get(status.TravelAgentId);
            if (travelagent != null)
            {
                travelagent.IsActive = status.IsActive;
                var updateTravelAgent = await _travelagentRepo.Update(travelagent);
                if (updateTravelAgent != null)
                    return status;
                return null;
            }
            return null;
        }




    }
}
