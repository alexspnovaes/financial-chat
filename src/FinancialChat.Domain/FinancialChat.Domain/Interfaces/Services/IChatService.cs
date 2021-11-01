using FinancialChat.Domain.Entities;
using FinancialChat.Domain.Models;
using FinancialChat.Domain.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Domain.Interfaces.Services
{
    public interface IChatService
    {
        Task<List<ChatRoomModel>> GetRooms();
        Task<List<MessageModel>> GetMessages(string roomId = "0", int offset = 0, int size = 50);
        Task SendMessage(UserInput user, MessageInput message);
    }
}
