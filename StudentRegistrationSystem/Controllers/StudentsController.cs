using Microsoft.AspNetCore.Authorization;
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
using System.Security.Claims;

namespace StudentRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly IUserRepository userRepository;
        private readonly IAddressRepository addressRepository;

        public StudentsController(IStudentRepository studentRepository, IUserRepository userRepository, IAddressRepository addressRepository)
        {
            this.studentRepository = studentRepository;
            this.userRepository = userRepository;
            this.addressRepository = addressRepository;
        }


        [HttpPost]
        public async Task<IActionResult> enterStudentDetails(Models.DTO.CreateStudentDTO request)
        {

            Console.WriteLine(request);

            //creating new Student
            var createUser = new User
            {
               
                userType = EnumRoles.Student,
                userStatus = true
               
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

            var studentAddress = new Address
            {
                studentID=createdUser.userID,   
                street = request.street,
                district = request.district,
                no = request.no
            };
            await userRepository.CreateAsync(createUser);
            //abstracting the implemetation to the repository
            await studentRepository.CreateAsync(createStudent);

            await addressRepository.CreateAsync(studentAddress);

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

        [HttpGet]
        [Authorize (Roles ="User")]
        [Route("GetStudentDetails")]
        public Task<Student> getStudentDetails()
        {
          
                string emails = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                return studentRepository.findStudentDetails(emails); ;
              
           
        }

        [HttpGet]
        [Route("GetAllStudents")]
        public List<Student> GetAllStudents()
        {

            List<Student> students = studentRepository.GetStudents();
            return students;

        }

        [HttpGet]
        [Route("GetStudentDetailswithoutAuthorize")]
        public Claim getStudentDetailswithoutAuthorize()
        {
            // The user identity can be accessed through the User property
            var userIdentity = User.Identity as ClaimsIdentity;
            Console.WriteLine(userIdentity);
            // Extract the user ID from the token
            var userIdClaim = userIdentity?.FindFirst("UserId");
            return userIdClaim;
        }

        [Authorize]
        [HttpPost]
        [Route("PostDetails")]
        public string AddUser(User user)
        {
            return "UserAdded with username";
        }

        [HttpPut]
        [Route("admin/updateStudentDetails")]  // Corrected the spelling in the Route attribute
        public async Task<Student> UpdatingStudentDetails(Models.DTO.StudentAddressDTO studentDTO)
        {
            Student student = new Student
            {
                studentID = studentDTO.studentID,
                firstName = studentDTO.firstName,
                lastName = studentDTO.lastName,
                phoneNumber = studentDTO.phoneNumber,
                gender =studentDTO .gender,
                academicProgramme = studentDTO.academicProgramme,
                birthday = studentDTO.birthday,
                enrolledDate = studentDTO.enrolledDate,
            };

            Address address = new Address
            {
                studentID = studentDTO.studentID,
                no = studentDTO.no,
                street = studentDTO.street,
                district = studentDTO.district
            };
            Student updatedStudent = await studentRepository.updateStudents(student);
            Address address1 = await addressRepository.updateAddress(address); // Corrected the variable name
            return updatedStudent;  // Return the updated course, not the input parameter
        }

        [HttpGet]
        [Route("admin/getStudentByID")]  // Corrected the spelling in the Route attribute
        public async Task<StudentAddressDTO> getStudentByID(int studentID)
        {

            StudentAddressDTO studentAddressDTO = await studentRepository.getStudentByID(studentID);  // Corrected the variable name
            return studentAddressDTO;  // Return the updated course, not the input parameter
        }

      
    }
}
