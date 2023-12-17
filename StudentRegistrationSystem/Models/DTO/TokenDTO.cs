using StudentRegistrationSystem.Models.Domain;

namespace StudentRegistrationSystem.Models.DTO
{
    public class TokenDTO
    {
        public string token { get; set; }
        public EnumRoles userType { get; set;}
    }
}
