using StudentRegistrationSystem.Models.Domain;

namespace StudentRegistrationSystem.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<Student> CreateAsync(Student student);

        Task<Student> findStudentDetails(String email);
    }
}