using FinancialChat.Domain.Hubs;
using FinancialChat.Domain.Interfaces;
using FinancialChat.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace FinancialChat.Domain.Services
{
    public class NotifyStockService : INotifyStockService
    {
        private readonly IChatHub _chatHub;

        public NotifyStockService(IChatHub chatHub)
        {
            _chatHub = chatHub;
        }

        public async Task NotifyRoomUserAsync(string code, string userId, string roomId, string value)
        {
            //TODO: Format string value number
            var message = $"{code} quote is ${value} per share";
            await _chatHub.SendMessage(message, roomId,"All", userId);
        }
    }
}
