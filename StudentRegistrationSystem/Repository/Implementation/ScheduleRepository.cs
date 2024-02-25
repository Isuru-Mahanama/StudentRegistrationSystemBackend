using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;
using StudentRegistrationSystem.Repository.Interface;
using AutoMapper;

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
            List<Schedulecs> schedulecs = applicationDbContext.schedulecs.ToList();
            List<Courses> courses = applicationDbContext.courses.ToList();
             // Filter the list based on the Status property
            List<Schedulecs> filteredSchedulecs = schedulecs.Where(s => s.scheduleStatus == true &&
                                                  courses.Any(c => c.courseCode == s.courseCode && c.courseStatus == true))
                                                  .ToList();

            return filteredSchedulecs;
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


        public async Task<Schedulecs> updateSchedules(ScheduleDTO schedulecs)
        {
            int scheduleID = schedulecs.scheduleID;
            var scheduleFromDatabase = await applicationDbContext.schedulecs.FirstOrDefaultAsync(u => u.scheduleID == scheduleID);
            if (scheduleFromDatabase != null)
            {
                Schedulecs sch = new Schedulecs();
                sch.scheduleID = scheduleID;
                sch.scheduleStatus = schedulecs.scheduleStatus;
                sch.courseCode = schedulecs.courseCode;
                sch.startTime = schedulecs.startTime;
                sch.endTime = schedulecs.endTime;
                sch.day = schedulecs.day;
                // Update the properties of the course entity using the DTO
                applicationDbContext.Entry(scheduleFromDatabase).CurrentValues.SetValues(sch);

                // Save the changes to the database
                await applicationDbContext.SaveChangesAsync();

                return sch; // Optional: You can return the updated course if needed
            }

            // If the course is not found, you might want to handle this scenario accordingly
            return null;
        }

        List<Schedulecs> IScheduleRepository.getScheduleByCourseCode(List<string> courseCodeList)
        {
            List<Schedulecs> scheduleList = applicationDbContext.schedulecs
            .Where(e => courseCodeList.Contains(e.courseCode) && e.scheduleStatus == true)
            .ToList();
            return scheduleList;
        }
    }
}
