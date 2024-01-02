using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;

namespace StudentRegistrationSystem.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<Student> CreateAsync(Student student);

        Task<Student> findStudentDetails(String email);
       
        public List<Student> GetStudents();
        Task<Student> updateStudents(Student student);
        public Task<StudentAddressDTO> getStudentByID(int studentID);

    }
}