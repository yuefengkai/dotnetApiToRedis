using System.Collections.Generic;
using System.Linq;
using dotnetApiToRedis.Data;
using dotnetApiToRedis.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        [HttpGet,Route("all"),SwaggerResponse(200,"成功返回list数据",typeof(List<ChatMessage>))]
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
        
        
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <returns></returns>
        [HttpPost,Route("add"),SwaggerResponse(200,"成功返回添加数据",typeof(ChatMessage))]
        public IActionResult Add([FromBody] ChatMessage chatMessage)
        {
            chatMessage = _redisData.Add(chatMessage);

            return new JsonResult(chatMessage);
        }
        
       /// <summary>
       /// 更新数据
       /// </summary>
       /// <param name="chatMessage"></param>
       /// <returns></returns>
        [HttpPost,Route("update"),SwaggerResponse(200,"成功返回修改后数据",typeof(ChatMessage))]
        public IActionResult Update([FromBody] ChatMessage chatMessage)
        {
            chatMessage = _redisData.Update(chatMessage);

            return new JsonResult(chatMessage);
        }
        
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost,Route("delete"),SwaggerResponse(200,"成功返回修信息",typeof(string))]
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
