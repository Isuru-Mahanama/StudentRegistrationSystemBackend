using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationSystem.Models.Domain
{
    public class Address
    {
        [Key]
        public long studentID { get; set; }
        public string no { get; set; }
        public string street { get; set; }
        public string district { get; set; }
    }
}
