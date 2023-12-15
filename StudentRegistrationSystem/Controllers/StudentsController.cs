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
                email = request.email,
                userType = EnumRoles.Student
            };

          //  await userRepository.CreateAsync(createUser);
            //Map the DTO to Domain model
            var createStudent = new Student
            {
                firstName = request.firstName,
                lastName = request.lastName,
                phoneNumber = request.phoneNumber,
                gender = request.gender,
                academicProgramme = request.academicProgramme,
                birthday = request.birthday ,
                enrolledDate = request.enrolledDate,
            };
                
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
               
            };

            return Ok(response);
        }
    }
}
