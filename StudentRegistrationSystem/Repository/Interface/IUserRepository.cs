﻿using StudentRegistrationSystem.Models.Domain;

namespace StudentRegistrationSystem.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
    }
}
