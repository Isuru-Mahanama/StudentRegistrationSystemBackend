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
            var userFromDatabase = dbContext.users.FirstOrDefault(u => u.email == email && u.passwordHash == passwordHash);
            return userFromDatabase;
        }
    }
}
