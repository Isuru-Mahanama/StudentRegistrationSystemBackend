﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<Courses>> AddingCourse([FromBody] CoursesDTO courses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cour = new Courses { 
                courseCode = courses.courseCode,
                courseName = courses.courseName,
                category = courses.category,
                level = courses.level,
                semester =  courses.semester,
                startDate = courses.startDate,
                endDate = courses.endDate,
                courseStatus = true
        };
            var course = await courseRepository.CreateAsync(cour);
            return Ok(course);
        }

        [HttpGet]
        [Route("GetAllCourses")]
        public List<Courses> GetAllCourses()
        {

            List<Courses> coureses = courseRepository.GetAllDetails();
            return coureses;

        }

        [HttpGet]
        [Route("GetAllCourseCodes")]
        public List<string> GetAllCoursCodes()
        {

            List<string> coureses = courseRepository.GetAllCourseCodes();
            return coureses;

        }

        [HttpPut]
        [Route("admin/updateCourseDetails")]  // Corrected the spelling in the Route attribute
        public async Task<Courses> UpdatingCourse([FromBody] CoursesDTO coursesDTO)
        {
            Courses updatedCourse = await courseRepository.updateCourse(coursesDTO);  // Corrected the variable name
            return updatedCourse;  // Return the updated course, not the input parameter
        }

        [HttpGet]
        [Route("admin/getCourseByCourseCode")]  // Corrected the spelling in the Route attribute
        public async Task<Courses> getCourseByCourseCode( String coursCode)
        {

            Courses updatedCourse = await courseRepository.getCourseByCourseCode(coursCode);  // Corrected the variable name
            return updatedCourse;  // Return the updated course, not the input parameter
        }

        [HttpDelete]
        [Route("admin/getCourseDelete")]  // Corrected the spelling in the Route attribute
        public async Task<Courses> deleteCourses(string courseCode)
        {

            Courses courses = await courseRepository.deleteCourse(courseCode);  // Corrected the variable name
            return courses;  // Return the updated course, not the input parameter
        }


    }


}
