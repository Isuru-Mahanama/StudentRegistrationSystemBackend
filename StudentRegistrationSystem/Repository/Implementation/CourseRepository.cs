using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Repository.Interface;
using System;

namespace StudentRegistrationSystem.Repository.Implementation
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CourseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Courses> CreateAsync(Courses courses)
        {

            await dbContext.courses.AddAsync(courses);
            await dbContext.SaveChangesAsync();
            return courses;
        }

        public List<Courses> GetAllDetails()
        {
            return dbContext.courses.ToList();
        }

        public List<string> GetAllCourseCodes()
        {
            // Assuming dbContext is an instance of your DbContext
            return dbContext.courses.Select(course => course.courseCode).ToList();
        }


    }
}
