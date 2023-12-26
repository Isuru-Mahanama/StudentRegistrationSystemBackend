using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;
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

        public List<Student> GetStudents()
        {
            return dbContext.students
         .Where(s => s.User.userStatus == true)
         .ToList();

        }

        public async Task<Student> updateStudents(Student student)
        {
            int studentID = student.studentID;
            var studentFromDatabase = await dbContext.students.FirstOrDefaultAsync(u => u.studentID == studentID);
            if (studentFromDatabase != null)
            {
                // Update the properties of the course entity using the DTO
                dbContext.Entry(studentFromDatabase).CurrentValues.SetValues(student);

                // Save the changes to the database
                await dbContext.SaveChangesAsync();

                return studentFromDatabase; // Optional: You can return the updated course if needed
            }

            // If the course is not found, you might want to handle this scenario accordingly
            return null;
        }

        public async Task<StudentAddressDTO> getStudentByID(int studentID)
        {
            var studentFromDatabase = await dbContext.students.FirstOrDefaultAsync(u => u.studentID == studentID);
            // If the course is not found, you might want to handle this scenario accordingly
            var addressFromDatabase = await dbContext.addresses.FirstOrDefaultAsync(a => a.studentID == studentID);
            var studentwithAddress = new StudentAddressDTO
            {
                studentID = studentID,
                firstName = studentFromDatabase.firstName,
                lastName = studentFromDatabase.lastName,
                phoneNumber = studentFromDatabase.phoneNumber,
                gender = studentFromDatabase.gender,
                academicProgramme = studentFromDatabase.academicProgramme,
                birthday = studentFromDatabase.birthday,
                enrolledDate = studentFromDatabase.enrolledDate,
                no = addressFromDatabase.no,
                street = addressFromDatabase.street,
                district = addressFromDatabase.district
       
    };
            return studentwithAddress;
        }

       
    }
}
