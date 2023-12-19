using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Repository.Interface;

namespace StudentRegistrationSystem.Repository.Implementation
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ScheduleRepository(ApplicationDbContext applicationDbContext) {
            this.applicationDbContext = applicationDbContext;
        }

       
        public async Task<Schedulecs> saveSchedules(Schedulecs schedulecs)
        {

            await applicationDbContext.schedulecs.AddAsync(schedulecs);
            await applicationDbContext.SaveChangesAsync();
            return schedulecs;
        }

    }
}
