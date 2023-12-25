using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationSystem.Models.Domain
{
    public class Schedulecs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int scheduleID { get; set; }
        public string courseCode { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string day { get; set; }
        public bool scheduleStatus { get; set; }
    }
}
