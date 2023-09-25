using LoginandSignup.Interfaces;
using LoginandSignup.Models;
using LoginandSignup.Models.DTO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace LoginandRegistration.Services
{
    public class ManageTravellerService : IManageTraveller
    {

        private readonly IRepo<Traveller, int> _travellerRepo;
        private readonly IRepo<User, int> _userRepo;
        private readonly IGenerateToken _tokenService;
        private readonly IRepo<TravelAgent, int> _travelagentRepo;

        public ManageTravellerService(IRepo<Traveller, int> travellerRepo, IRepo<User, int> userRepo, IGenerateToken tokenService, IRepo<TravelAgent, int> travelagentRepo)
        {

            _travellerRepo = travellerRepo;
            _userRepo = userRepo;
            _tokenService = tokenService;
            _travelagentRepo = travelagentRepo;
        }

        public async Task<UserDTO> TravellerRegistration(TravellerDTO user)
        {
            UserDTO myUser = null;
            var hmac = new HMACSHA512();
            user.Users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordClear ?? "1234"));
            user.Users.PasswordKey = hmac.Key;
            user.Users.Role = "traveller";

            var users = await _travellerRepo.GetAll();
            if (users != null)
            {
                var myAdminUser = users.FirstOrDefault(u => u.Username == user.Username && u.PhoneNumber == user.PhoneNumber);
                if (myAdminUser != null)
                {
                    return null;
                }
            }
            var userResult = await _userRepo.Add(user.Users);
            var travellerResult = await _travellerRepo.Add(user);
            if (userResult != null && travellerResult != null)
            {
                myUser = new UserDTO();
                myUser.UserId = travellerResult.TravellerId;
                myUser.Role = userResult.Role;
                myUser.Token = _tokenService.GenerateToken(myUser);
            }
            return myUser;
        }




    }
}
