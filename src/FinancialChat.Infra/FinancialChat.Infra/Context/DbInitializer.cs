using FinancialChat.Domain.Entities;
using FinancialChat.Domain.Models.Inputs;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinancialChat.Infra.Data.Context
{
    public static class DbInitializer
    {
        public static async Task Seed(IServiceScope serviceScope)
        {
            //var redis = serviceScope.ServiceProvider.GetService<IConnectionMultiplexer>();
            //var redisDatabase = redis.GetDatabase();
            //var totalUsersKeyExist = await redisDatabase.KeyExistsAsync("total_users");
            //if (!totalUsersKeyExist)
            //{
            //    await redisDatabase.StringSetAsync("total_users", 0);
            //    await redisDatabase.StringSetAsync("room:0:name", "General");

            //    {
            //        var rnd = new Random();
            //        Func<double> rand = () => rnd.NextDouble();
            //        Func<int> getTimestamp = () => (int)DateTimeOffset.Now.ToUnixTimeSeconds();


            //        var greetings = new List<string>() { "Hello", "Hi", "Yo", "Hola" };

            //        var messages = new List<string>() {
            //            "Hello!",
            //            "Hi, How are you? What about our next meeting?",
            //            "Yeah everything is fine",
            //            "Next meeting tomorrow 10.00AM",
            //            "Wow that's great"
            //        };
            //        Func<string> getGreeting = () => greetings[(int)Math.Floor(rand() * greetings.Count)];
            //        var addMessage = new Func<string, string, string, int, Task>(async (string roomId, string fromId, string content, int timeStamp) =>
            //        {
            //            var roomKey = $"room:{roomId}";
            //            var message = new MessageInput()
            //            {
            //                From = fromId,
            //                Date = timeStamp,
            //                Message = content,
            //                RoomId = roomId
            //            };
            //            await redisDatabase.SortedSetAddAsync(roomKey, JsonSerializer.Serialize(message), (double)message.Date);
            //        });

            //    }
            //}
        }
    }
}
