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
        public async Task<Address> updateAddress(Address address)
        {
            int studentID = address.studentID;
            var addressFromDatabase = await dbContext.addresses.FirstOrDefaultAsync(u => u.studentID == studentID);
            if (addressFromDatabase != null)
            {
                // Update the properties of the course entity using the DTO
                dbContext.Entry(addressFromDatabase).CurrentValues.SetValues(address);

                // Save the changes to the database
                await dbContext.SaveChangesAsync();

                return addressFromDatabase; // Optional: You can return the updated course if needed
            }

            // If the course is not found, you might want to handle this scenario accordingly
            return null;
        }
    }
}
