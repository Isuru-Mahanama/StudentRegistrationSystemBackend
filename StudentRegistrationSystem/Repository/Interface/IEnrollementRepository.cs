using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;

namespace StudentRegistrationSystem.Repository.Interface
{
    public interface IEnrollementRepository
    {
        Task<Enrollement> CreateAsync(Enrollement enrollement);
        Task<EnrolledDetailsDTO> unEnroll(EnrolledDetailsDTO enrolledDetailsDTO);
        List<string> findEnrolledCoursesByID(int studentID);
    }
}
