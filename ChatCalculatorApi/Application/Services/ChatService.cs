using Core.Entities;
using Core.Interfaces;
using System.Data;

namespace Application.Services
{
    public class ChatService
    {
        private readonly IChatRepository _chatRepo;

        public ChatService(IChatRepository chatRepo)
        {
            _chatRepo = chatRepo;
        }

        public ChatHistory ProcessExpression(int userId, string expression)
        {
            var resultNum = EvaluateExpression(expression);
            var resultWords = NumberToWords(resultNum);

            var history = new ChatHistory
            {
                UserId = userId,
                Expression = expression,
                ResultNumber = resultNum.ToString(),
                ResultWords = resultWords
            };

            _chatRepo.SaveChat(history);
            return history;
        }

        private double EvaluateExpression(string exp)
        {
            var dt = new DataTable();
            var val = dt.Compute(exp, "");
            return Convert.ToDouble(val);
        }

        private string NumberToWords(double num)
        {
            if (num == 0) return "Zero";
            if (num == 1) return "One";
            if (num == 2) return "Two";
            if (num == 6) return "Six";

            return num.ToString();
        }
    }
}
