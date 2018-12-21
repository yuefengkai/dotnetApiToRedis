using System;

namespace dotnetApiToRedis.Models
{
    public class ChatMessage
    {
        public long Id { get; set; }
        
        public string ChatRoom { get; set; }
        
        public DateTime DateTime { get; set; }
        
        public string Content { get; set; }
        
        public DateTime AddTime { get; set; }
        
        public bool Status { get; set; }
    }
}
