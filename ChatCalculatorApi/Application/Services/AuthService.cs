using Core.Entities;
using Core.Interfaces;
using BCrypt.Net;

namespace Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepo;

        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public bool Register(string username, string password)
        {
            var existing = _userRepo.GetUserByUsername(username);
            if (existing != null) return false;

            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            _userRepo.AddUser(new User { Username = username, PasswordHash = hash });
            return true;
        }

        public User? Login(string username, string password)
        {
            var user = _userRepo.GetUserByUsername(username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }

        public void ChangePassword(int userId, string newPassword)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _userRepo.ChangePassword(userId, hash);
        }
    }
}
