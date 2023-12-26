using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRegistrationSystem.Models.Domain
{
    public class Enrollement
    {
        [ForeignKey("coursCode")]
        public string coursCode { get; set; }

        [ForeignKey("userID")]
        public int userID { get; set; }
        public bool enrollementStatus { get; set; }
        public User user { get; set; }
        public Courses courses { get; set; }

    }
}
