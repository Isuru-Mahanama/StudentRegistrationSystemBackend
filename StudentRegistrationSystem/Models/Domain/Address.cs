using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRegistrationSystem.Models.Domain
{
    public class Address
    {
        [ForeignKey("Student")]
        public int studentID { get; set; }
        public string no { get; set; }
        public string street { get; set; }
        public string district { get; set; }
        public User user { get; set; }
    }
}
