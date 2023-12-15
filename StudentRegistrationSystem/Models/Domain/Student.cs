using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRegistrationSystem.Models.Domain
{
    public class Student
    {
        [Key, ForeignKey("studentID")]
        public int studentID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
     
        public int phoneNumber { get; set; }
        public string gender { get; set; }
        public string academicProgramme { get; set; }
        public DateTime birthday { get; set; }
        public DateTime enrolledDate { get; set;}

        public User User { get; set; }

    }
}
