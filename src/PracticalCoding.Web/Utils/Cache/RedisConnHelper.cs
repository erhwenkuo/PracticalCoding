using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Utils.Cache
{
    public class RedisConnHelper
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string redisConfigKey = "redis:ConnectionConfig";
            string redisConnSetting = "localhost";
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[redisConfigKey]))
            {
                redisConnSetting = ConfigurationManager.AppSettings[redisConfigKey];
            }
            return ConnectionMultiplexer.Connect(redisConnSetting);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}