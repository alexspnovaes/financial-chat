using FinancialChat.Domain.Interfaces.Services;
using FinancialChat.Domain.Models.Inputs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinancialChat.UI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatHub(IChatService chatService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task OnConnectedAsync()
        {

            var httpContext = Context.GetHttpContext();
            await httpContext.Session.LoadAsync();
            var userStr = httpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(userStr))
            {
                var user = System.Text.Json.JsonSerializer.Deserialize<UserInput>(userStr);

                await _userService.OnStartSession(user);

            }
            else
            {
                await OnDisconnectedAsync(new Exception("Not Authorized"));
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            await httpContext.Session.LoadAsync();
            var userStr = httpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(userStr))
            {
                var user = System.Text.Json.JsonSerializer.Deserialize<UserInput>(userStr);

                await _userService.OnStopSession(user);

            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message, string roomId)
        {
            var user = _httpContextAccessor.HttpContext?.User.Identity.Name;

            var msg = new MessageInput
            {
                Date = (int)DateTimeOffset.Now.ToUnixTimeSeconds(),
                From = user,
                Message = message,
                RoomId = roomId
            };

            await _chatService.SendMessage(msg);
        }
    }
}
