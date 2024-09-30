using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerJob.Application.Redis
{
    public interface IRedisContext
    {
        void Clear();
        public ConnectionMultiplexer Connect();
        Task<bool> Exists(int db, string key);
        Task<T> GetAsync<T>(int db, string key);
        public IDatabase GetDb(int db = 0);
        Task Remove(int db, string key);
        Task<bool> SaveAsync<T>(int db, string key, T data, TimeSpan? expiry);
    }
}
