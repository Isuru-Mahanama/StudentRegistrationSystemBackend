using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Repository.Implementation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


        public async Task GenerateEmailAsync(DbContext dbContext)
        {
            // Assuming dbContext is your Entity Framework DbContext
            // Save the entity to the database
            dbContext.Add(this);
            await dbContext.SaveChangesAsync();

            // Now, you can use the generated userID to create the email
            email = GenerateOrderedEmail(userID);
        }

        private static string GenerateOrderedEmail(int userID)
        {
           
            // Customize the logic for generating ordered emails based on your requirements
            string prefix = "m";
            string suffix = "@example.com";
            string orderedEmail = $"{prefix}{userID}{suffix}";

            return orderedEmail;
        }
    }
}
