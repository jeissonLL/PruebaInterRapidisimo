namespace Application.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }
    }
}
