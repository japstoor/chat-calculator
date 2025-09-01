using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ChatHistory
    {
        public int HistoryId { get; set; }
        public int UserId { get; set; }
        public string Expression { get; set; } = string.Empty;
        public string ResultNumber { get; set; } = string.Empty;
        public string ResultWords { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public class ChatRequest
    {
        public int UserId { get; set; }
        public string Expression { get; set; } = string.Empty;
    }

}
