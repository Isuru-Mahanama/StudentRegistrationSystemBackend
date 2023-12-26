using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRegistrationSystem.Models.DTO
{
    public class EnrolledDetailsDTO
    {
        public string coursCode { get; set; }
        public int userID { get; set; }
    }
}
