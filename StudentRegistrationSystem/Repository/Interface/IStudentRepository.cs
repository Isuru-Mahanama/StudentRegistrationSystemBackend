using StudentRegistrationSystem.Models.Domain;

namespace StudentRegistrationSystem.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<Student> CreateAsync(Student student);
    }
}
