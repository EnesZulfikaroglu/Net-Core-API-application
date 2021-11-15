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


            var _redisHost = "172.18.0.3";
            var _port = 6379;

            _redisEndpoint = new RedisEndpoint(_redisHost, _port);

        }

        // Key Redis veritabanında var ise true döner
        public bool IsKeyExist(string key)
        {
            using (var client = new RedisClient(_redisEndpoint))
            {
                return client.ContainsKey(key);
            }
        }

        // Key set edilir
        public void SetKeyValue(string key, string value)
        {

            using (var client = new RedisClient(_redisEndpoint))
            {

                client.SetValue(key, value);

            }

        }

        // Key karşılık değeri geriye döndürülür.
        public string GetKeyValue(string key)
        {

            using (var client = new RedisClient(_redisEndpoint))
            {

                return client.GetValue(key);
            }

        }

        // Generic yapıda key kontrolü
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
                throw;
            }

        }

        // Generic: Key karşılık değeri döndürülür.
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
