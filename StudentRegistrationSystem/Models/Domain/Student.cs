using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationSystem.Models.Domain
{
    public class Student
    {
        [Key]
        public long studentID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public int phoneNumber { get; set; }
        public string gender { get; set; }
        public string academicProgramme { get; set; }
        public DateOnly birthday { get; set; }
        public DateOnly enrolledDate { get; set;}

    }
}
