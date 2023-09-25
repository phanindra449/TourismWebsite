namespace LoginandSignup.Models.DTO
{
    public class AdminDTO :Admin
    {

        public AdminDTO()
        {
            Users = new User();
        }

        public string? PasswordClear { get; set; }

    }
}
