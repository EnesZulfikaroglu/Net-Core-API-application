using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Cache
{
    public interface IRedisService
    {
        bool CheckConnectionWithTimeLimit(TimeSpan timeSpan);
        void CheckConnection();
        bool IsKeyExist(string key);

        void SetKeyValue(string key, string value);

        string GetKeyValue(string key);

        bool StoreList<T>(string key, T Value, TimeSpan timeout);

        T GetList<T>(string key);
    }
}
