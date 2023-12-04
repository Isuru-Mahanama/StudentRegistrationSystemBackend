using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Models.Domain;
using System.Net.Sockets;

namespace StudentRegistrationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        //codefirst Approach - creating the table
        public DbSet<Student> students { get; set; }
        public DbSet<Address> addresses { get; set; }
    }
}
