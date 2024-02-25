using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;
using StudentRegistrationSystem.Repository.Implementation;
using StudentRegistrationSystem.Repository.Interface;
using System.Security.Claims;

namespace StudentRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollementController : ControllerBase
    {
        private readonly IEnrollementRepository enrollementRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IScheduleRepository scheduleRepository;

        public EnrollementController(IEnrollementRepository enrollementRepository,
                                     IStudentRepository studentRepository,
                                     IScheduleRepository scheduleRepository) {
            this.enrollementRepository = enrollementRepository;
            this.studentRepository = studentRepository;
            this.scheduleRepository = scheduleRepository;
        }
        [HttpPost]
        [Route("user/enrollement")]
        public async Task<ActionResult<Enrollement>> AddingEnrolledCourses([FromBody] EnrolledDetailsDTO enrolledDetailsDTO )
        {
            
            Console.WriteLine(enrolledDetailsDTO);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingEnrollment = await enrollementRepository.GetByCoursCodeAndUserIdAsync(enrolledDetailsDTO.coursCode, enrolledDetailsDTO.userID);
            if (existingEnrollment != null && existingEnrollment.enrollementStatus == true)
            {
                // Enrollment already exists, handle accordingly (e.g., return a conflict response)
                return Conflict("Enrollment already exists.");
            }
            var enrollement = new Enrollement()
            {
                coursCode = enrolledDetailsDTO.coursCode,
                userID = enrolledDetailsDTO.userID,
                enrollementStatus = true
            };
            var enrolled = await enrollementRepository.CreateAsync(enrollement);
            
            return Ok(enrolled);
        }

        [HttpPut("user/unEnrollement")]
        public async Task<ActionResult<EnrolledDetailsDTO>> UnenrollCourses([FromBody] EnrolledDetailsDTO enrolledDetailsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingEnrollment = await enrollementRepository.GetByCoursCodeAndUserIdAsync(enrolledDetailsDTO.coursCode, enrolledDetailsDTO.userID);
            if (existingEnrollment != null && existingEnrollment.enrollementStatus == false)
            {
                // Enrollment already exists, handle accordingly (e.g., return a conflict response)
                return Conflict("No enrollemnet to  relevent course.");
            }
            var enrolled = await enrollementRepository.unEnroll(enrolledDetailsDTO);
            return Ok(enrolled);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("GetTimeTableDetails")]
        public async Task<List<Schedulecs>> GetTimeTableDetails()
        {
            string email = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            Task<Student> studentTask = studentRepository.findStudentDetails(email);
            Student student = await studentTask;

            List<string> courseCodeList = enrollementRepository.findEnrolledCoursesByID(student.studentID);
            List<Schedulecs> schedulecs = scheduleRepository.getScheduleByCourseCode(courseCodeList);
            return schedulecs;
        }

    }
}
