using LoginandSignup.Interfaces;
using LoginandSignup.Models.DTO;
using LoginandSignup.Models;
using System.Security.Cryptography;
using System.Text;
using LoginandSignup.Models.Context;

namespace LoginandSignup.Services
{

    public class ManageUserService : IManageUser
    {
        private readonly IRepo<User, int> _userRepo;
        private readonly IRepo<Admin, int> _adminRepo;
        private readonly IRepo<Traveller, int> _travellerRepo;
        private readonly IRepo<TravelAgent,int> _travelagentRepo;

        private readonly IGenerateToken _tokenService;

        public ManageUserService(
                                 IRepo<User, int> userRepo,
                                 IGenerateToken tokenService, IRepo<Admin, int> adminrepo, IRepo<Traveller, int> travellerRepo, IRepo<TravelAgent, int> travelagentRepo)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _adminRepo = adminrepo;
            _travellerRepo = travellerRepo;
            _travelagentRepo = travelagentRepo;
        }
        public async Task<UserDTO> Login(UserDTO userDTO)
        {
            UserDTO user = null;
            var users = await _userRepo.GetAll();
            var userData = users.FirstOrDefault(u => u.Email == userDTO.Email);
            if (userData != null)
            {
                var hmac = new HMACSHA512(userData.PasswordKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userPass[i] != userData.PasswordHash[i])
                        return null;
                }
                user = new UserDTO();
                user.UserId = userData.UserId;
                user.Role = userData.Role;
                if (user.Role != "travelagent")
                {
                    user.Token =  _tokenService.GenerateToken(user);
                    return user;
                }
                var doctor = await _travelagentRepo.Get(user.UserId);
                if (doctor != null && doctor.IsActive==false)
                {
                    return user;
                }
                user.Token = (_tokenService.GenerateToken(user));
                return user;

            }
            return null;
          
        }

        public async Task<UserDTO> AdminRegistration(AdminDTO user)
        {
            UserDTO myUser = null;
            var hmac = new HMACSHA512();
            user.Users.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordClear ));
            user.Users.PasswordKey = hmac.Key;
            user.Users.Role = "Admin";

            var users = await _adminRepo.GetAll();
            if (users != null)
            {
                var myAdminUser = users.FirstOrDefault(u => u.Email == user.Email && u.PhoneNumber == user.PhoneNumber);
                if (myAdminUser != null)
                {
                    return null;
                }
            }
            var userResult = await _userRepo.Add(user.Users);
            var adminResult = await _adminRepo.Add(user);
            if (userResult != null && adminResult != null)
            {
                myUser = new UserDTO();
                myUser.UserId = adminResult.AdminId;
                myUser.Role = userResult.Role;
                myUser.Token = _tokenService.GenerateToken(myUser);
            }
            return myUser;
        }

        public async Task<User?> GetByEmail(string email)
        {

            var users = await _userRepo.GetAll();

            var user =  users.FirstOrDefault(u=>u.Email==email);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("Database is empty");
            }
        }








    }
}
