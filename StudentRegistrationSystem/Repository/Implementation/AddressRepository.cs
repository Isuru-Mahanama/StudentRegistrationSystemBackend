using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Repository.Interface;

namespace StudentRegistrationSystem.Repository.Implementation
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AddressRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<Address> CreateAsync(Address address)
        {
            //Domain model to DTO

            await dbContext.addresses.AddAsync(address);
            await dbContext.SaveChangesAsync();

            return address;
        }
    }
}
