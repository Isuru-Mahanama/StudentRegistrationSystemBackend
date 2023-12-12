﻿namespace StudentRegistrationSystem.Models.DTO
{
    public class CreateStudentDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int phoneNumber { get; set; }
        public string gender { get; set; }
        public string academicProgramme { get; set; }
        public DateTime birthday { get; set; }
        public DateTime enrolledDate { get; set; }
        public string no { get; set; }
        public string street { get; set; }
        public string district { get; set; }
    }
}
