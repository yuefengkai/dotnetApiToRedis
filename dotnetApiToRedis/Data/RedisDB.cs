using System;
using System.Collections.Generic;
using dotnetApiToRedis.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace dotnetApiToRedis.Data
{
    public class RedisDB
    {
        private static IRedisClient _redisClient = null;

        private static IRedisClient RedisClient
        {
            get
            {
                if (_redisClient != null)
                {
                    return _redisClient;
                }

                var redisManger = new RedisManagerPool("db.gzz.cn:6379"); //Redis的连接字符串
                _redisClient = redisManger.GetClient(); //获取一个Redis Client

                return _redisClient;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ChatMessage> GetALl()
        {
            var redisTodos = RedisClient.As<ChatMessage>();
            return redisTodos.GetAll();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ChatMessage Add(ChatMessage chatMessage)
        {
            var redisTodos = RedisClient.As<ChatMessage>();
            return  redisTodos.Store(chatMessage);        
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ChatMessage Update(ChatMessage chatMessage)
        { 
            var redisTodos = RedisClient.As<ChatMessage>();
            
             var saveTodo = redisTodos.GetById(chatMessage.Id);               //根据Id查询        查
            
            "Saved Todo: {0}".Print(saveTodo.Dump());
 
            saveTodo.AddTime = chatMessage.AddTime;   
            saveTodo.Status = chatMessage.Status;                                         //改
            return redisTodos.Store(saveTodo);
        }

        public void Delete(int id)
        { 
            var redisTodos = RedisClient.As<ChatMessage>();
            
            redisTodos.DeleteById(id);                           //删除
        }
    }
}
