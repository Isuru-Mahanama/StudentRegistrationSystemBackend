using StudentRegistrationSystem.Models.Domain;

namespace StudentRegistrationSystem.Repository.Interface
{
    public interface ICourseRepository
    {
        Task<Courses> CreateAsync(Courses courses);
        public List<Courses> GetAllDetails();
        public List<string> GetAllCourseCodes();
        Task<Courses> updateCourse(Courses courses);
        Task<Courses> getCourseByCourseCode(String courseCode);
       
    }
}
