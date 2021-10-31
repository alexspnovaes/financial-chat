using FinancialChat.Domain.Entities;
using FinancialChat.Domain.Interfaces;
using FinancialChat.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialChat.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinancialChatContext _context;

        public UserRepository(FinancialChatContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(User user) => await _context.Users.AddAsync(user);

        public async Task<User> GetUserByIdAsync(Guid id) => await _context.Users.FindAsync(id);

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();
    }
}
