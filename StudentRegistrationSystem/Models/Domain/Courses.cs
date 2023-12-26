using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationSystem.Models.Domain
{
    public class Courses
    {
       
        public string courseName { get; set; }
        [Key]
        public string courseCode { get; set; }
        public int semester { get; set; }
        public int level { get; set; }
        public  string category { get; set; }
        public  DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool courseStatus { get; set; }
        public Enrollement enrollement { get; set; }

    }
}
