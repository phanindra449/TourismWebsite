using LoginandSignup.Models.DTO;
namespace LoginandSignup.Interfaces
{
    public interface IManageUser
    {
        public Task<UserDTO?> Login(UserDTO user);
        public Task<UserDTO?> AdminRegistration(AdminDTO adminDTO);






    }
}
