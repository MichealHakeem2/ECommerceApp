using ECommerceApp.Core.Entities;
using ECommerceApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> ValidateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return null;
            return user.PasswordHash == password ? user : null; // in real app, hash
        }

        public async Task RegisterUserAsync(string fullName, string email, string password)
        {
            var user = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = password,
                Role = "customer",
                CreatedAt = DateTime.Now
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<IEnumerable<User>> GetAdminsAsync()
        {
            return await _userRepository.GetAdminsAsync();
        }
    }
}
