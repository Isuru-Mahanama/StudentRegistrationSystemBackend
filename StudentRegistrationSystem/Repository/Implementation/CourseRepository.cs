﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Courses> updateCourse( Courses courses)
        {
            string courseCode = courses.courseCode;
            var courseFromDatabase = await dbContext.courses.FirstOrDefaultAsync(u => u.courseCode == courseCode);
            if (courseFromDatabase != null)
            {
                // Update the properties of the course entity using the DTO
                dbContext.Entry(courseFromDatabase).CurrentValues.SetValues(courses);

                // Save the changes to the database
                await dbContext.SaveChangesAsync();

                return courseFromDatabase; // Optional: You can return the updated course if needed
            }

            // If the course is not found, you might want to handle this scenario accordingly
            return null;
        }


        public async Task<Courses> getCourseByCourseCode(string coursesCode)
        {
            var courseFromDatabase = await dbContext.courses.FirstOrDefaultAsync(u => u.courseCode == coursesCode);
            // If the course is not found, you might want to handle this scenario accordingly
            return courseFromDatabase;
        }
        
    }
}
