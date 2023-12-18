﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentRegistrationSystem.Data;

#nullable disable

namespace StudentRegistrationSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231218053420_createdAddressForeignKey3")]
    partial class createdAddressForeignKey3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("passwordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userType")
                        .HasColumnType("int");

                    b.HasKey("userID");

                    b.ToTable("users");
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.Address", b =>
                {
                    b.HasOne("StudentRegistrationSystem.Models.Domain.Student", "student")
                        .WithOne("address")
                        .HasForeignKey("StudentRegistrationSystem.Models.Domain.Address", "studentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("student");
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

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.Student", b =>
                {
                    b.Navigation("address")
                        .IsRequired();
                });

            modelBuilder.Entity("StudentRegistrationSystem.Models.Domain.User", b =>
                {
                    b.Navigation("Student")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
