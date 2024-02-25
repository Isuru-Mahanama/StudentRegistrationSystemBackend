using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Controllers;
using StudentRegistrationSystem.Helper;
using StudentRegistrationSystem.Models.DTO;
using StudentRegistrationSystem.Repository.Implementation;
using StudentRegistrationSystem.Repository.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace StudentRegistrationSystem.Models.Domain
{
    public class User
    {
        private readonly IEmailService emailService;

        public string? email { get; set; } 
        public string passwordHash{ get; set; } 
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int userID { get; set; }
        public EnumRoles userType { get; set; }
        [JsonIgnore]
        public Student Student { get; set; }
        [JsonIgnore]
        public Address address { get; set; }
        public bool userStatus { get; set; }
        [JsonIgnore]
        public ICollection<Enrollement> enrollement { get; set; }
        [JsonIgnore]
        public ICollection<Courses> courses { get; set; }
        public DateTime createdDate { get; set; }
        public User(IEmailService emailService)
        {
            this.emailService = emailService;
            passwordHash = GenerateRandomPassword();
            createdDate = DateTime.Now;
        }

        public User()
        {
           
        }

        private  string GenerateRandomPassword()
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            // Generate a random password with four characters
            char[] password = new char[4];
            for (int i = 0; i < 4; i++)
            {
                password[i] = allowedChars[random.Next(allowedChars.Length)];
            }
            var createdPassword = new string(password);
            sendMail(createdPassword).Wait();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(createdPassword);

            // studentsController.sendMail();
            return passwordHash;
           
        }


        public async  Task<IActionResult> sendMail(string createdPassword)
        {
            StudentsController studentsController = new StudentsController();
            
            try
            {
                MailRequest mailRequest = new MailRequest();
                mailRequest.ToEmail = "isuruanamika@gmail.com";
                mailRequest.subject = "Welcome To University Of Arizona..";
                mailRequest.body = "Thanks for registering. Your password is "+createdPassword;
                
                await emailService.SendEmailAsync(mailRequest);
                return new OkResult();

            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }

}

