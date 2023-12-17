using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Repository.Interface;

namespace StudentRegistrationSystem.Repository.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public StudentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Student> CreateAsync(Student student)
        {
            //Domain model to DTO

            await dbContext.students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return student;
        }

        public async Task<Student> findStudentDetails(string email)
        {
            var userFromDatabase = await dbContext.users.FirstOrDefaultAsync(u => u.email == email);
            if (userFromDatabase != null)
            {
               int userID =  userFromDatabase.userID;
               var studentFromDatabase = await dbContext.students.FirstOrDefaultAsync(s => s.studentID == userID);
                return studentFromDatabase;
            }

            else
            {
                return new Student();
            }
            return new Student();
        }
    }
}
