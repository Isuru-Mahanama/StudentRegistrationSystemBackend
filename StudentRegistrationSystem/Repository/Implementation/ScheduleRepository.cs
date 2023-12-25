using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;
using StudentRegistrationSystem.Repository.Interface;

namespace StudentRegistrationSystem.Repository.Implementation
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ScheduleRepository(ApplicationDbContext applicationDbContext) {
            this.applicationDbContext = applicationDbContext;
        }

       
        public async Task<Schedulecs> saveSchedules(ScheduleDTO schedulecs)
        {
            var schedulec = new Schedulecs{
                courseCode = schedulecs.courseCode,
                startTime = schedulecs.startTime,
                endTime = schedulecs.endTime,
                day = schedulecs.day,
                scheduleStatus = true
            };
            await applicationDbContext.schedulecs.AddAsync(schedulec);
            await applicationDbContext.SaveChangesAsync();
            return schedulec;
        }

        public List<Schedulecs> GetSchedulecs()
        {
            return applicationDbContext.schedulecs.ToList();
        }
        
        public async Task<Schedulecs> deleteSchedule(int scheduleID)
        {
            var scheduleFromDatabase = await applicationDbContext.schedulecs.FirstOrDefaultAsync(u => u.scheduleID == scheduleID);
            // If the course is not found, you might want to handle this scenario accordingly
            scheduleFromDatabase.scheduleStatus =false;

            await applicationDbContext.SaveChangesAsync();
            return scheduleFromDatabase;
        }

        public async Task<Schedulecs> getScheduleByID(int scheduleID)
        {
            var scheduleFromDatabase = await applicationDbContext.schedulecs.FirstOrDefaultAsync(u => u.scheduleID == scheduleID);
            // If the course is not found, you might want to handle this scenario accordingly
            return scheduleFromDatabase;
        }


        public async Task<Schedulecs> updateSchedules(Schedulecs schedulecs)
        {
            int scheduleID = schedulecs.scheduleID;
            var scheduleFromDatabase = await applicationDbContext.schedulecs.FirstOrDefaultAsync(u => u.scheduleID == scheduleID);
            if (scheduleFromDatabase != null)
            {
                // Update the properties of the course entity using the DTO
                applicationDbContext.Entry(scheduleFromDatabase).CurrentValues.SetValues(schedulecs);

                // Save the changes to the database
                await applicationDbContext.SaveChangesAsync();

                return schedulecs; // Optional: You can return the updated course if needed
            }

            // If the course is not found, you might want to handle this scenario accordingly
            return null;
        }
    }
}
