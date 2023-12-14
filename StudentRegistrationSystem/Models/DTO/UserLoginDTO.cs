namespace StudentRegistrationSystem.Models.DTO
{
    public class UserLoginDTO
    {
        public required string email { get; set; }
        public required string passwordHash { get; set; }
    }
}
