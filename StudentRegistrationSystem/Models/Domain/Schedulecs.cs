using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        [JsonIgnore]
        public Courses course { get; set; }
        public DateTime createdDateSchedulecs { get; set; }
        public Schedulecs()
        {
            createdDateSchedulecs = DateTime.Now; // Initialize createdDate with current date and time
        }
    }
}
