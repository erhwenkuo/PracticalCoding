using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jil;
using System.IO;
using System.Text;

namespace PracticalCoding.Web.Utils.Cache
{
    public static class RedisExtentions
    {
        /// <summary>
        /// 透過鍵值(key)從Cache中取回特定C#型態(type)的物件
        /// </summary>
        /// <typeparam name="T">物件的型態(C# type)</typeparam>
        /// <param name="cache">Redis Cache的連線物件</param>
        /// <param name="key">要從Cache取出物件的鍵值</param>
        /// <returns>C#物件</returns>
        public static T Get<T>(this IDatabase cache, string key)
        {
            return Deserialize<T>(cache.StringGet(key));
        }

        /// <summary>
        /// 透過鍵值(key)從Cache中取回C#物件
        /// </summary>
        /// <param name="cache">Redis Cache的連線物件</param>
        /// <param name="key">要從Cache取出物件的鍵值</param>
        /// <returns>C#物件</returns>
        public static object Get(this IDatabase cache, string key)
        {
            return Deserialize<object>(cache.StringGet(key));
        }

        /// <summary>
        /// 把物件instance指定鍵值地儲放到Cache中
        /// </summary>
        /// <param name="cache">>Redis Cache的連線物件</param>
        /// <param name="key">要儲存到Cache中物件的鍵值</param>
        /// <param name="value">>C#物件</param>
        public static void Set(this IDatabase cache, string key, object value)
        {
            cache.StringSet(key, Serialize(value));
        }


        /// <summary>
        /// 把C#的物件instance序列化成byte[]
        /// </summary>
        /// <param name="o">要被序列化的C#物件instance(注意:要被序列化的物件的類別必需要宣告成[Serializable])</param>
        /// <returns>byte[]</returns>
        static byte[] Serialize(object o)
        {
            if (o == null)
                return null;

            var strValue = JSON.Serialize(o, Options.ISO8601);
            return Encoding.UTF8.GetBytes(strValue);
        }

        /// <summary>
        /// 把byte[]的資料stream反序列化成為C#某特定型態的物件instance
        /// </summary>
        /// <typeparam name="T">反序列化後要轉換的C#物件型態</typeparam>
        /// <param name="stream">byte[]的資料stream</param>
        /// <returns>C#特定型態的物件instance</returns>
        static T Deserialize<T>(byte[] value)
        {
            if (value==null)
                return default(T);

            var strValue = value != null ? Encoding.UTF8.GetString(value) : null;
            return JSON.Deserialize<T>(strValue, Options.ISO8601);
        }
    }
}