﻿namespace StudentRegistrationSystem.Models.Domain
{
    public class User
    {
        public string email { get; set; } = string.Empty;
        public string passwordHash{ get; set; } = string.Empty;
    }
}
