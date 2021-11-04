using FinancialChat.Domain.Interfaces;
using FinancialChat.Domain.Interfaces.ExternalServices;
using FinancialChat.Domain.Interfaces.Services;
using FinancialChat.Domain.Models.Inputs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FinancialChat.Domain.Hubs
{
    public class ChatHub : Hub, IChatHub
    {
        const string stockMessage = "/stock=";
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStockApiExternalService _stockApiExternalService;

        public ChatHub(IChatService chatService, IUserService userService, IHttpContextAccessor httpContextAccessor, IStockApiExternalService stockApiExternalService)
        {
            _chatService = chatService ?? chatService;
            _userService = userService ?? userService;
            _httpContextAccessor = httpContextAccessor;
            _stockApiExternalService = stockApiExternalService;
        }

        public override async Task OnConnectedAsync()
        {
            
            var httpContext = Context.GetHttpContext();
            await httpContext.Session.LoadAsync();
            
            var roomId = httpContext.Request.Query["chatroomId"];
            var userName = _httpContextAccessor.HttpContext?.User.Identity.Name;

            if (userName == null)
                await OnDisconnectedAsync(new Exception("Not Authorized"));
            else
            {
                var user = new UserInput { Username = userName };
                await _userService.OnStartSession(user, roomId);
                await Clients.All.SendAsync($"chatroom{roomId}", user.Username);

            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            await httpContext.Session.LoadAsync();
            
            var roomId = httpContext.Request.Query["chatroomId"];
            var userName = _httpContextAccessor.HttpContext?.User.Identity.Name;

            if (userName == null)
                await base.OnDisconnectedAsync(exception);
            else
            {
                var user = new UserInput { Username = userName };
                await _userService.OnStopSession(user, roomId);

            }
        }

        public async Task SendMessage(string message, string roomId, string userTo, string user = null)
        {
            if (string.IsNullOrEmpty(user))
                user = _httpContextAccessor.HttpContext?.User.Identity.Name;

            if (!string.IsNullOrEmpty(user))
            {
                if (!await ProcessStockCodeAsync(message, roomId, user))
                {
                    var msg = new MessageInput
                    {
                        Date = (int)DateTimeOffset.Now.ToUnixTimeSeconds(),
                        From = user,
                        To = userTo,
                        Message = message,
                        RoomId = roomId
                    };

                    await _chatService.SendMessage(msg);
                }
            }
            else
            {
                await OnDisconnectedAsync(new Exception("Not Authorized"));
            }
        }

        private async Task<bool> ProcessStockCodeAsync(string message, string roomId, string user)
        {
            if (message.Contains(stockMessage))
            {
                var stockCode = message.Substring(message.LastIndexOf(stockMessage) + 7, 7);
                var stock = new StockInput { RoomId = roomId, StockCode = stockCode, User = user };
                await _stockApiExternalService.PostAsync(stock);
                return true;
            }

            return false;
        }
    }
}
