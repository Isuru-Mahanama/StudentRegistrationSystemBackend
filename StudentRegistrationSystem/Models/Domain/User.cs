using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Repository.Implementation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentRegistrationSystem.Models.Domain
{
    public class User
    {
       
        public string? email { get; set; } 
        public string passwordHash{ get; set; } = GenerateRandomPassword();
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int userID { get; set; }
        public EnumRoles userType { get; set; }
        [JsonIgnore]
        public Student Student { get; set; }
      


        private static string GenerateRandomPassword()
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            // Generate a random password with four characters
            char[] password = new char[4];
            for (int i = 0; i < 4; i++)
            {
                password[i] = allowedChars[random.Next(allowedChars.Length)];
            }

            return new string(password);
        }
       
    }
}
