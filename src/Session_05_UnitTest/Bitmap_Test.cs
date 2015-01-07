using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace Session_05_UnitTest
{
    [TestClass]
    public class Bitmap_Test
    {
        [TestMethod]
        public void TestOneMillionBitCount()
        {
            //連接到Local的Redis Server
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase redisDb = redis.GetDatabase();
            
            //設定要儲存Key
            var keyFormat = "{0}:{1}";
            var metricName = "daily_active_users";
            var dateString = "2014-12-23"; // DateTime.Today.ToString("yyyy-MM-dd");
            var key = string.Format(keyFormat, metricName, dateString);
            //假設我們有一百萬個User,而且UserId是使用數字
            var totalUsers = 1000000;
            //先初始一個Key/Value來存放一百萬個User的BitArray
            redisDb.StringSetBit(key, totalUsers, false);
            //我們使用模擬使用者10萬次的登入
            var stopwatch = new Stopwatch();
            var totalIterCount = 100000;
            stopwatch.Start(); //開始計時
            for (long i = 0; i < totalIterCount; i++)
            {
                var userId = i;
                //redisDb.StringSetBitAsync(key, userId, true, CommandFlags.FireAndForget);
                redisDb.StringSetBitAsync(key, userId, true);
            }
            stopwatch.Stop(); //停止
            Debug.WriteLine("Redis finsh "+ totalIterCount+ " ops in " 
                + stopwatch.ElapsedMilliseconds + " ms!");

            //我們看一下算一下daily_active_users的count需要花多少時間
            stopwatch.Reset();
            stopwatch.Start();
            var task = redisDb.StringBitCountAsync(key);
            var daily_active_users = redisDb.Wait(task);
            stopwatch.Stop();
            Debug.WriteLine("Redis finsh 1 million daily_active_users count ["
                + daily_active_users +"] in " 
                + stopwatch.ElapsedMilliseconds + " ms!");
            Debug.WriteLine("Test Complete!");

            Assert.AreEqual(100000, daily_active_users);
        }

        [TestMethod]
        public void TestOneMillionBitCountByDateRange()
        {
            //連接到Local的Redis Server
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase redisDb = redis.GetDatabase();

            //設定要儲存Key
            var keyFormat = "{0}:{1}";
            var metricName = "daily_active_users";
            //var dateString = "2014-12-23"; // DateTime.Today.ToString("yyyy-MM-dd");
            var dateStringRange = new string[]{"2014-12-01","2014-12-02", "2014-12-03"
                , "2014-12-04", "2014-12-05", "2014-12-06", "2014-12-07"};

            for (int i=0; i<dateStringRange.Length; i++)
            {
                var dateString = dateStringRange[i];
                var key = string.Format(keyFormat, metricName, dateString);
                //假設我們有一百萬個User,而且UserId是使用數字
                var totalUsers = 1000000;
                //先初始一個Key/Value來存放一百萬個User的BitArray
                redisDb.StringSetBit(key, totalUsers, false);

                //我們使用模擬使用者10萬次的登入
                var stopwatch = new Stopwatch();
                var totalIterCount = 100000;
                stopwatch.Start(); //開始計時
                for (long j = 0; j < totalIterCount; j++)
                {
                    var userId = i*totalIterCount + j;
                    redisDb.StringSetBitAsync(key, userId, true);
                }
                stopwatch.Stop(); //停止
                Debug.WriteLine("Redis finsh [#"+ i +"] " + totalIterCount + " ops in "
                    + stopwatch.ElapsedMilliseconds + " ms!");
            }
            

            //我們看一下算一下一週的daily_active_users (unique)的count需要花多少時間
            var stopwatch2 = new Stopwatch();

            stopwatch2.Start();
            var redisKeys = new List<RedisKey>();
            for (int i=0; i<dateStringRange.Length; i++){
                var dateString = dateStringRange[i];
                var key = string.Format(keyFormat, metricName, dateString);
                redisKeys.Add(key);
            }

            var weekKey = string.Format(keyFormat, metricName, "2014-12-01:2014-12-07");
            //先用BitMap的OR來計算所有的Bit OR起來的結果來回放到一個新的BitMap (weekKey)
            redisDb.StringBitOperation(Bitwise.Or, weekKey, redisKeys.ToArray());

            //算一下一週的weekly_active_users (unique)的population count
            var task = redisDb.StringBitCountAsync(weekKey);
            var weekly_active_users = redisDb.Wait(task);

            stopwatch2.Stop();
            Debug.WriteLine("Redis finsh 1 million weekly_active_users count ["
                + weekly_active_users + "] in "
                + stopwatch2.ElapsedMilliseconds + " ms!");
            Debug.WriteLine("Test Complete!");

            Assert.AreEqual(700000, weekly_active_users);
        }
    }
}
