using Core.Entities;
using Core.Interfaces;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public User? GetUserByUsername(string username)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand("SELECT * FROM Users WHERE Username=@username", conn);
            cmd.Parameters.AddWithValue("@username", username);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    UserId = (int)reader["UserId"],
                    Username = reader["Username"].ToString(),
                    PasswordHash = reader["PasswordHash"].ToString()
                };
            }
            return null;
        }

        public void AddUser(User user)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(
                "INSERT INTO Users (Username, PasswordHash) VALUES (@username, @passwordHash)", conn);
            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@passwordHash", user.PasswordHash);

            cmd.ExecuteNonQuery();
        }

        public void ChangePassword(int userId, string newPasswordHash)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(
                "UPDATE Users SET PasswordHash=@passwordHash WHERE UserId=@userId", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@passwordHash", newPasswordHash);

            cmd.ExecuteNonQuery();
        }
    }

}
