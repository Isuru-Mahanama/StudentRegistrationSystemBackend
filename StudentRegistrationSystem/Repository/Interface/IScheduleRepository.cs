using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;

namespace StudentRegistrationSystem.Repository.Interface
{
    public interface IScheduleRepository
    {
        Task<Schedulecs> saveSchedules(ScheduleDTO schedulecs);
        public List<Schedulecs> GetSchedulecs();

        public Task<Schedulecs> getScheduleByID(int scheduleID);
        Task<Schedulecs> updateSchedules(Schedulecs schedulecs);
        Task<Schedulecs> deleteSchedule(int scheduleID);
    }
}
