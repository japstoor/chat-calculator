using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly string _connectionString;

        public ChatRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void SaveChat(ChatHistory history)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(
                "INSERT INTO ChatHistory (UserId, Expression, ResultNumber, ResultWords) VALUES (@userId, @exp, @resNum, @resWords)", conn);

            cmd.Parameters.AddWithValue("@userId", history.UserId);
            cmd.Parameters.AddWithValue("@exp", history.Expression);
            cmd.Parameters.AddWithValue("@resNum", history.ResultNumber);
            cmd.Parameters.AddWithValue("@resWords", history.ResultWords);

            cmd.ExecuteNonQuery();
        }

        public List<ChatHistory> GetHistoryByUser(int userId)
        {
            var list = new List<ChatHistory>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand("SELECT * FROM ChatHistory WHERE UserId=@userId ORDER BY CreatedAt DESC", conn);
            cmd.Parameters.AddWithValue("@userId", userId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ChatHistory
                {
                    HistoryId = (int)reader["HistoryId"],
                    UserId = (int)reader["UserId"],
                    Expression = reader["Expression"].ToString(),
                    ResultNumber = reader["ResultNumber"].ToString(),
                    ResultWords = reader["ResultWords"].ToString(),
                    CreatedAt = (DateTime)reader["CreatedAt"]
                });
            }

            return list;
        }
    }
}
