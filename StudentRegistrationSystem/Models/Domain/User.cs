using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRegistrationSystem.Models.Domain
{
    public class User
    {
        [Key]
        public string email { get; set; } = string.Empty;
        public string passwordHash{ get; set; } = string.Empty;
        public int userID { get; set; }
        public EnumRoles userType { get; set; }



    }
}
