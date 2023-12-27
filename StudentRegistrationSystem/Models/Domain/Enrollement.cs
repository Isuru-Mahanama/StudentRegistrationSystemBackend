using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentRegistrationSystem.Models.Domain
{
    public class Enrollement
    {
        [ForeignKey("coursCode")]
        public string coursCode { get; set; }

        [ForeignKey("userID")]
        public int userID { get; set; }
        public bool enrollementStatus { get; set; }
        [JsonIgnore]
        public User user { get; set; }
        [JsonIgnore]
        public Courses courses { get; set; }

    }
}
