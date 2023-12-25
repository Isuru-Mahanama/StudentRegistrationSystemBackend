using StudentRegistrationSystem.Models.Domain;

namespace StudentRegistrationSystem.Repository.Interface
{
    public interface IAddressRepository
    {
        Task<Address> CreateAsync(Address address);
        Task<Address> updateAddress(Address address);
    }
}
