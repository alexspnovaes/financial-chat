using FinancialChat.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace FinancialChat.Infra.Context
{
    public class FinancialChatContext : IdentityDbContext<User>
    {
        public FinancialChatContext(DbContextOptions<FinancialChatContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ChatRoom>().HasData(
                new ChatRoom
                {
                     Id = Guid.NewGuid(),
                     Name = "Financial Chat Room 1"
                },
                new ChatRoom
                {
                    Id = Guid.NewGuid(),
                    Name = "Financial Chat Room 2"
                }
            );
        }
    }
}
