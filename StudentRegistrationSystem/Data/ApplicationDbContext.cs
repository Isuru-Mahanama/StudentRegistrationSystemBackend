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
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.userID);
            modelBuilder.Entity<Student>()
                .HasKey(s => s.studentID);

            modelBuilder.Entity<Student>()     // Assuming userID is the shared primary key
                .HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.studentID);


            modelBuilder.Entity<Address>()
                .HasKey(a => a.studentID);

            modelBuilder.Entity<Address>()     // Assuming userID is the shared primary key
                .HasOne(a => a.user)
                .WithOne(s => s.address)
                .HasForeignKey<Address>(a => a.studentID);
            base.OnModelCreating(modelBuilder);
        }


    }
}
