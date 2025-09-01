using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IChatRepository
    {
        void SaveChat(ChatHistory history);
        List<ChatHistory> GetHistoryByUser(int userId);
    }

}
