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
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository courseRepository;

        public CourseController(ICourseRepository courseRepository) {
            this.courseRepository = courseRepository;
        }
        [HttpPost("admin/course")]
        public async Task<ActionResult<Courses>> AddingCourse([FromBody] Courses courses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await courseRepository.CreateAsync(courses);
            return Ok(course);
        }

        [HttpGet]
        [Route("GetAllCourses")]
        public List<Courses> GetAllCourses()
        {

            List<Courses> coureses = courseRepository.GetAllDetails();
            return coureses;

        }

    }

  
}
