using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Repository.Interface;

namespace StudentRegistrationSystem.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            if(user.email == null)
            {
                await dbContext.AddAsync(user);
            }
            //Domain model to DTO

            await dbContext.SaveChangesAsync();

            return user;
        }

        public User GetUserFromDatabase(string email, string passwordHash)
        {
            var userFromDatabase = dbContext.users.FirstOrDefault(u => u.email == email );
            bool passwordMatches = BCrypt.Net.BCrypt.Verify(passwordHash, userFromDatabase.passwordHash);
            if (passwordMatches) {
                return userFromDatabase;
            }
            return null;
        }

        public async Task<User> deleteStudent(int studentID)
        {
            var userFromDatabase = await dbContext.users.FirstOrDefaultAsync(u => u.userID == studentID);
            // If the course is not found, you might want to handle this scenario accordingly
            userFromDatabase.userStatus = false;

            await dbContext.SaveChangesAsync();
            return userFromDatabase;
        }
    }
}
