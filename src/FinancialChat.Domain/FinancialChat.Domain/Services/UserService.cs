using FinancialChat.Domain.Interfaces.Services;
using FinancialChat.Domain.Models;
using FinancialChat.Domain.Models.Inputs;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialChat.Domain.Services
{
    public class UserService : IUserService
    {
        protected readonly IDatabase _database;
        protected readonly IConnectionMultiplexer _redis;
        protected readonly IMessageService _messageService;
        public UserService(IConnectionMultiplexer redis, IMessageService messageService)
        {
            _redis = redis; 
            _database = redis.GetDatabase();
            _messageService = messageService;
        }

        public async Task<IDictionary<string, UserModel>> Get(int[] ids)
        {
            var users = new Dictionary<string, UserModel>();
            foreach (var id in ids)
            {
                users.Add(id.ToString(), new UserModel()
                {
                    Id = id,
                    Username = await _database.HashGetAsync($"user:{id}", "username"),
                    IsOnline = await _database.SetContainsAsync("online_users", id.ToString())
                });
            }
            return users;
        }

        public async Task<IDictionary<string, UserModel>> GetOnline()
        {
            var onlineIds = await _database.SetMembersAsync("online_users");
            var users = new Dictionary<string, UserModel>();
            foreach (var onlineIdRedisValue in onlineIds)
            {
                var onlineId = onlineIdRedisValue.ToString();
                var user = await _database.HashGetAsync($"user:{onlineId}", "username");
                users.Add(onlineId, new UserModel()
                {
                    Id = int.Parse(onlineId),
                    Username = user.ToString(),
                    IsOnline = true
                });
            }

            return users;
        }

        public async Task OnStartSession(UserInput user)
        {
            await _database.SetAddAsync("online_users", user.Id);
            user.IsOnline = true;
            await _messageService.PublishMessage("user.connected", user);
        }

        public async Task OnStopSession(UserInput user)
        {
            await _database.SetRemoveAsync("online_users", user.Id);
            user.IsOnline = false;
            await _messageService.PublishMessage("user.disconnected", user);
        }
    }
}
