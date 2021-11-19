using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Cache
{
    public class RedisManager : IRedisService
    {
        RedisEndpoint _redisEndpoint;

        public RedisManager()
        {


            var _redisHost = "redis"; // The name of Redis container
            var _port = 6379;

            _redisEndpoint = new RedisEndpoint(_redisHost, _port);

        }

        // Returns true if connected successfully
        public bool CheckConnection()
        {
            using (var client = new RedisClient(_redisEndpoint))
            {
                try
                {
                    return client.Ping();
                }
                catch
                {
                    Console.WriteLine("Connection Error");
                    return false;
                }
            }
        }

        // Returns true if Key is exist on Redis database
        public bool IsKeyExist(string key)
        {
            using (var client = new RedisClient(_redisEndpoint))
            {
                return client.ContainsKey(key);
            }
        }

        // Setting Key and value
        public void SetKeyValue(string key, string value)
        {

            using (var client = new RedisClient(_redisEndpoint))
            {

                client.SetValue(key, value);

            }

        }

        // Getting the value of key
        public string GetKeyValue(string key)
        {

            using (var client = new RedisClient(_redisEndpoint))
            {

                return client.GetValue(key);
            }

        }

        // Generic Build - Setting
        public bool StoreList<T>(string key, T Value, TimeSpan timeout)
        {

            try
            {

                using (var client = new RedisClient(_redisEndpoint))
                {

                    client.As<T>().SetValue(key, Value, timeout);
                }
                return true;
            }

            catch (Exception)
            {
                return false;
            }

        }

        // Generic Build - Getting
        public T GetList<T>(string key)
        {

            T result;

            using (var client = new RedisClient(_redisEndpoint))
            {

                var wrapper = client.As<T>();
                result = wrapper.GetValue(key);

            }

            return result;
        }
    }
}
