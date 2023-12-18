using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationSystem.Models.DTO
{
    public class CoursesDTO
    {
        public string courseName { get; set; }
        public string courseCode { get; set; }

        public int semester { get; set; }

        public int level { get; set; }
        public string category { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
