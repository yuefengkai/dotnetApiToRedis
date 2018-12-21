using System.Collections.Generic;
using System.Linq;
using dotnetApiToRedis.Data;
using dotnetApiToRedis.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnetApiToRedis.Controllers
{
    [Route("api/chatMsg")]
    [ApiController]
    public class ChatMessageController:ControllerBase
    {
        private readonly RedisDB _redisData;
        public ChatMessageController(RedisDB redisData)
        {
            _redisData = redisData;
        }

        // GET api/values
        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<ChatMessage>> Get()
        {
            //return new List<ChatMessage>();
            
            var list = _redisData.GetALl();
            if (list == null || !list.Any())
            {
                return new List<ChatMessage>();
            }

            return list.ToList();
        }
        
        
        // GET api/values
        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] ChatMessage chatMessage)
        {
            chatMessage = _redisData.Add(chatMessage);

            return new JsonResult(chatMessage);
        }
        
        // GET api/values
        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] ChatMessage chatMessage)
        {
            chatMessage = _redisData.Update(chatMessage);

            return new JsonResult(chatMessage);
        }
        
        // GET api/values
        [HttpGet]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            _redisData.Delete(id);

            return new JsonResult(new {result = 1, msg = "删除成功"});
        }

        
        // GET api/values
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("getById")]
        public IActionResult GetById()
        {
            return Ok("1");
        }
    }
}
