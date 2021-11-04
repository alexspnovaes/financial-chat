using FinancialChat.Domain.Entities;
using FinancialChat.Domain.Interfaces;
using FinancialChat.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialChat.Infra.Data.Repositories
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly FinancialChatContext _context;

        public ChatRoomRepository(FinancialChatContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<ChatRoom>> GetAllAsync() => await _context.ChatRooms.ToListAsync();
    }
}
