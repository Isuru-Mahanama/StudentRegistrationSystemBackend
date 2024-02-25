﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentRegistrationSystem.Data;

#nullable disable

namespace StudentRegistrationSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoursesUser", b =>
                {
                    b.Property<string>("coursescourseCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("usersuserID")
                        .HasColumnType("int");

                    b.HasKey("coursescourseCode", "usersuserID");

                    b.HasIndex("usersuserID");

                    b.ToTable("CoursesUser");
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.Address", b =>
                {
                    b.Property<int>("studentID")
                        .HasColumnType("int");

                    b.Property<string>("district")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("no")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("studentID");

                    b.ToTable("addresses");
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.Courses", b =>
                {
                    b.Property<string>("courseCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("courseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("courseStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("level")
                        .HasColumnType("int");

                    b.Property<int>("semester")
                        .HasColumnType("int");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime2");

                    b.HasKey("courseCode");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.Enrollement", b =>
                {
                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.Property<string>("coursCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("enrollementStatus")
                        .HasColumnType("bit");

                    b.HasKey("userID", "coursCode");

                    b.HasIndex("coursCode");

                    b.ToTable("enrollements");
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.Schedulecs", b =>
                {
                    b.Property<int>("scheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("scheduleID"));

                    b.Property<string>("courseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("createdDateSchedulecs")
                        .HasColumnType("datetime2");

                    b.Property<string>("day")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("endTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("scheduleStatus")
                        .HasColumnType("bit");

                    b.Property<string>("startTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("scheduleID");

                    b.HasIndex("courseCode");

                    b.ToTable("schedulecs");
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.Student", b =>
                {
                    b.Property<int>("studentID")
                        .HasColumnType("int");

                    b.Property<string>("academicProgramme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("birthday")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("enrolledDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("phoneNumber")
                        .HasColumnType("int");

                    b.HasKey("studentID");

                    b.ToTable("students");
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.User", b =>
                {
                    b.Property<int>("userID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userID"));

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("passwordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("userStatus")
                        .HasColumnType("bit");

                    b.Property<int>("userType")
                        .HasColumnType("int");

                    b.HasKey("userID");

                    b.ToTable("users");
                });

            modelBuilder.Entity("CoursesUser", b =>
                {
                    b.HasOne("StudentRegistrationSystem.Models.Domain.Courses", null)
                        .WithMany()
                        .HasForeignKey("coursescourseCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentRegistrationSystem.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("usersuserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.Address", b =>
                {
                    b.HasOne("StudentRegistrationSystem.Models.Domain.User", "user")
                        .WithOne("address")
                        .HasForeignKey("StudentRegistrationSystem.Models.Domain.Address", "studentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.Enrollement", b =>
                {
                    b.HasOne("StudentRegistrationSystem.Models.Domain.Courses", "courses")
                        .WithMany("enrollement")
                        .HasForeignKey("coursCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentRegistrationSystem.Models.Domain.User", "user")
                        .WithMany("enrollement")
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("courses");

                    b.Navigation("user");
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.Schedulecs", b =>
                {
                    b.HasOne("StudentRegistrationSystem.Models.Domain.Courses", "course")
                        .WithMany("schedulecs")
                        .HasForeignKey("courseCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.Student", b =>
                {
                    b.HasOne("StudentRegistrationSystem.Models.Domain.User", "User")
                        .WithOne("Student")
                        .HasForeignKey("StudentRegistrationSystem.Models.Domain.Student", "studentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.Courses", b =>
                {
                    b.Navigation("enrollement");

                    b.Navigation("schedulecs");
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.User", b =>
                {
                    b.Navigation("Student")
                        .IsRequired();

                    b.Navigation("address")
                        .IsRequired();

                    b.Navigation("enrollement");
                });
#pragma warning restore 612, 618
        }
    }
}
