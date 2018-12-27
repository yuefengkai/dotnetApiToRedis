using System;

namespace dotnetApiToRedis.Models
{
    public class ChatMessage
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 群聊名称
        /// </summary>
        public string ChatRoom { get; set; }
        
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime DateTime { get; set; }
        
        /// <summary>
        /// 发送内容
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
    }
}
