using StackExchange.Redis;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace EmployerJob.Application.Redis
{
    public class RedisContext : IRedisContext
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        public RedisContext(string host)
        {
            var options = ConfigurationOptions.Parse(host);
            options.ConnectRetry = 5;
            options.AllowAdmin = true;

            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() =>
               ConnectionMultiplexer.Connect(options));
        }

        public ConnectionMultiplexer Connect()
        {
            return _connectionMultiplexer.Value;
        }

        public IDatabase GetDb(int db = 0) => Connect().GetDatabase(db);

        public async Task<bool> Exists(int db, string key) => await GetDb(db).KeyExistsAsync(key);

        public async Task<bool> SaveAsync<T>(int db, string key, T data, TimeSpan? expiry)
        {
            var value = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });

            return await GetDb(db).StringSetAsync(key, value, expiry);
        }

        public async Task<T> GetAsync<T>(int db, string key)
        {
            var data = await GetDb(db).StringGetAsync(key);

            if (data.IsNullOrEmpty)
                return default(T);

            return JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
        }

        public async Task Remove(int db, string key) => await GetDb(db).KeyDeleteAsync(key);

        public void Clear()
        {
            var endpoints = Connect().GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = Connect().GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }
    }
}
