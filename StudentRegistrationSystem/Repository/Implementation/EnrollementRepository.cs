﻿using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;
using StudentRegistrationSystem.Repository.Interface;

namespace StudentRegistrationSystem.Repository.Implementation
{
    public class EnrollementRepository : IEnrollementRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EnrollementRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Enrollement> CreateAsync(Enrollement enrollement)
        {
            Enrollement filteredEnroll = dbContext.enrollements.
                                         FirstOrDefault(e => e.coursCode == enrollement.coursCode && e.userID == enrollement.userID);
            if (filteredEnroll == null)
            {
                await dbContext.enrollements.AddAsync(enrollement);
                await dbContext.SaveChangesAsync();
            }
            if (filteredEnroll != null)
            {
                dbContext.Entry(filteredEnroll).CurrentValues.SetValues(enrollement);
                await dbContext.SaveChangesAsync();
            }

            return enrollement;
        }
        public async Task<EnrolledDetailsDTO> unEnroll(EnrolledDetailsDTO enrolledDetailsDTO)
        {
            //Domain model to DTO

            var enrollementFromDatabase = await dbContext.enrollements.
                                           FirstOrDefaultAsync(u => u.userID == enrolledDetailsDTO.userID &&
                                            u.coursCode == enrolledDetailsDTO.coursCode);
            if (enrollementFromDatabase != null)
            {
                enrollementFromDatabase.enrollementStatus = false;
                // Update the properties of the course entity using the DTO
                dbContext.Entry(enrollementFromDatabase).CurrentValues.SetValues(enrollementFromDatabase);

                // Save the changes to the database
                await dbContext.SaveChangesAsync();

                return enrolledDetailsDTO; 
            }

            return null;

           
        }
        public async Task<Enrollement> GetByCoursCodeAndUserIdAsync(string coursCode, int userId)
        {
            // Assuming Enrollement has a property named 'coursCode' and 'userID'
            return await dbContext.enrollements
                .FirstOrDefaultAsync(e => e.coursCode == coursCode && e.userID == userId);
        }
        List<string> IEnrollementRepository.findEnrolledCoursesByID(int studentID)
        {
            List<Courses> courses = dbContext.courses.ToList();

            List<string> courseCodeList = dbContext.enrollements
            .Where(e => e.userID == studentID && e.enrollementStatus==true)
            .Select(e => e.coursCode)
            .ToList();

            // Filter the courseCodeList based on the courseStatus
            List<string> enrolledCoursesWithTrueStatus = (from courseCode in courseCodeList
                                                          join course in courses
                                                          on courseCode equals course.courseCode
                                                          where course.courseStatus
                                                          select courseCode).ToList();

            return enrolledCoursesWithTrueStatus;
        }
    }
}
