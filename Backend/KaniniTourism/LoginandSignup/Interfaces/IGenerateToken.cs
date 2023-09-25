using LoginandSignup.Models.DTO;

namespace LoginandSignup.Interfaces
{
    public interface IGenerateToken


    {
        public string GenerateToken(UserDTO user);

    }
}
