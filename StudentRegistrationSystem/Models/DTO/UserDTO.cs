﻿namespace StudentRegistrationSystem.Models.DTO
{
    public class UserDTO
    {
        public required string email { get; set; } 
        public required string passwordHash { get; set; } 

        public required int userID { get; set; }
        public required string userType { get; set; } 
    }
}
