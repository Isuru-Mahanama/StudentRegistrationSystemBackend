using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;

namespace StudentRegistrationSystem.Repository.Interface
{
    public interface ICourseRepository
    {
        Task<Courses> CreateAsync(Courses courses);
        public List<Courses> GetAllDetails();
        public List<string> GetAllCourseCodes();
        Task<Courses> updateCourse(CoursesDTO coursesDTO);

        Task<Courses> getCourseByCourseCode(String courseCode);
        Task<Courses> deleteCourse(string courseCode);
    }
}
