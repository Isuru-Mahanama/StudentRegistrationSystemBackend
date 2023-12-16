using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Identity.Client;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;
using StudentRegistrationSystem.Repository.Implementation;
using StudentRegistrationSystem.Repository.Interface;
using System.Reflection;

namespace StudentRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly IUserRepository userRepository;    

        public StudentsController(IStudentRepository studentRepository, IUserRepository userRepository)
        {
            this.studentRepository = studentRepository;
            this.userRepository = userRepository;
        }


        [HttpPost]
        public async Task<IActionResult> enterStudentDetails(Models.DTO.CreateStudentDTO request)
        {

            Console.WriteLine(request);

            //creating new Student
            var createUser = new User
            {
               
                userType = EnumRoles.Student
            };

            User createdUser = await userRepository.CreateAsync(createUser);

              string GenerateOrderedEmail(int userID)
              {

                // Customize the logic for generating ordered emails based on your requirements
                string prefix = "m";
                string suffix = "@example.com";
                string orderedEmail = $"{prefix}{userID}{suffix}";

                return orderedEmail;
               }
            string email = GenerateOrderedEmail(createdUser.userID);
            //Map the DTO to Domain model
            var createStudent = new Student
            {
                studentID = createdUser.userID,
                firstName = request.firstName,
                lastName = request.lastName,
                phoneNumber = request.phoneNumber,
                gender = request.gender,
                academicProgramme = request.academicProgramme,
                birthday = request.birthday ,
                enrolledDate = request.enrolledDate,
            };
             createdUser.email = email;
            await userRepository.CreateAsync(createUser);
            //abstracting the implemetation to the repository
            await studentRepository.CreateAsync(createStudent);

            var response = new StudentDTO
            {
                studentID = createStudent.studentID,
                firstName = createStudent.firstName,
                lastName = createStudent.lastName,
                phoneNumber = createStudent.phoneNumber,
                gender = createStudent.gender,
                academicProgramme = createStudent.academicProgramme,
                birthday = createStudent.birthday,
                enrolledDate = createStudent.enrolledDate,
                email = createdUser.email,
                passwordHash = createdUser.passwordHash
            };

            return Ok(response);
        }


    }
}
