using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByUsername(string username);
        void AddUser(User user);
        void ChangePassword(int userId, string newPasswordHash);
    }

}
