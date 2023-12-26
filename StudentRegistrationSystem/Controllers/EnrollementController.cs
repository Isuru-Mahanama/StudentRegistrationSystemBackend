using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;
using StudentRegistrationSystem.Repository.Implementation;
using StudentRegistrationSystem.Repository.Interface;

namespace StudentRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollementController : ControllerBase
    {
        private readonly IEnrollementRepository enrollementRepository;

        public EnrollementController(IEnrollementRepository enrollementRepository) {
            this.enrollementRepository = enrollementRepository;
        }
        [HttpPost("user/enrollement")]
        public async Task<ActionResult<Enrollement>> AddingEnrolledCourses([FromBody] EnrolledDetailsDTO enrolledDetailsDTO )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
            
            var enrolled = await enrollementRepository.unEnroll(enrolledDetailsDTO);
            return Ok(enrolled);
        }

    }
}
