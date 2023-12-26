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
        public DbSet<Courses> courses { get; set; }
        public DbSet<Schedulecs> schedulecs { get; set; }
        public DbSet<Enrollement> enrollements { get; set; }

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

            modelBuilder.Entity<Enrollement>()
                .HasKey(e => new { e.userID, e.coursCode });

            modelBuilder.Entity<Enrollement>()     
                .HasOne(e => e.user)
                .WithOne(u => u.enrollement)
                .HasForeignKey<Enrollement>(e => e.userID);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Courses>()
               .HasKey(c => c.courseCode);

            modelBuilder.Entity<Enrollement>()     
                .HasOne(e => e.courses)
                .WithOne(c => c.enrollement)
                .HasForeignKey<Enrollement>(e => e.coursCode);
            base.OnModelCreating(modelBuilder);

        }


    }
}
