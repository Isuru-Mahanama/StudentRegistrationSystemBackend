using StudentRegistrationSystem.Models.Domain;

namespace StudentRegistrationSystem.Repository.Interface
{
    public interface IScheduleRepository
    {
        Task<Schedulecs> saveSchedules(Schedulecs schedulecs);
    }
}
