using FinancialChat.Domain.Interfaces.Services;
using FinancialChat.Domain.Models;
using FinancialChat.Domain.Models.Inputs;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IConnectionMultiplexer redis, IMessageService messageService, IHttpContextAccessor httpContextAccessor)
        {
            _redis = redis;
            _database = redis.GetDatabase();
            _messageService = messageService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<string>> GetOnlineUsersAsync(string roomId, bool excludeMe = false)
        {
            var userName = _httpContextAccessor.HttpContext?.User.Identity.Name;

            var key = $"room:{roomId}:online_users";
            var roomExists = await _database.KeyExistsAsync(key);
            var users = new List<string>();

            if (!roomExists)
            {
                return users;
            }
            var values = await _database.SetMembersAsync(key);

            foreach (var valueRedisVal in values)
            {
                var value = valueRedisVal.ToString();
                users.Add(value);
            }

            if (excludeMe)
                users.Remove(userName);
            return users;
        }


        public async Task OnStartSession(UserInput user, string roomId)
        {
            await _database.SetAddAsync($"room:{roomId}:online_users", user.Username);
            user.IsOnline = true;
            await _messageService.PublishMessage("user.connected", user);
        }

        public async Task OnStopSession(UserInput user, string roomId)
        {
            await _database.SetRemoveAsync($"room:{roomId}:online_users", user.Username);
            user.IsOnline = false;
            await _messageService.PublishMessage("user.disconnected", user);
        }
    }
}
