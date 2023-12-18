using StudentRegistrationSystem.Models.Domain;

namespace StudentRegistrationSystem.Repository.Interface
{
    public interface ICourseRepository
    {
        Task<Courses> CreateAsync(Courses courses);
        public List<Courses> GetAllDetails();
    }
}
