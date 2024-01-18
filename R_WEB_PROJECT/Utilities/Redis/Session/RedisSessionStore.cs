using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace R_WEB_PROJECT.Utilities.Redis.Session
{
    public class RedisSessionStore
    {
        private readonly IDistributedCache _distributedCache;

        public RedisSessionStore(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        //세션 셋팅
        public async Task SetSessionAsync<T>(string key, T value, TimeSpan expirationTime)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime
            };

            string serializedValue = JsonSerializer.Serialize(value);
            await _distributedCache.SetStringAsync(key, serializedValue, options);
            Log.Log.Debug("REDIS", string.Format("SET key :{0} / serializedValue = {{{1}}}", key, serializedValue));
        }

        //세션 불러오기
        public async Task<T> GetSessionAsync<T>(string key)
        {
            var serializedValue = await _distributedCache.GetStringAsync(key);
            if (serializedValue == null)
                return default;

            Log.Log.Debug("REDIS", string.Format("GET key :{0} / serializedValue = {{{1}}}", key, serializedValue));

            var returnValue = JsonSerializer.Deserialize<T>(serializedValue);

            return returnValue;
        }

        //세션 삭제
        public async Task RemoveSessionAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }
    }
}
