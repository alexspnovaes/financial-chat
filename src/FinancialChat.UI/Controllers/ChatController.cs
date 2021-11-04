using FinancialChat.Domain.Interfaces.Services;
using FinancialChat.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialChat.UI.Controllers
{

    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public ChatController(IChatService chatService, IUserService userService, IConfiguration configuration)
        {
            _chatService = chatService;
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var rooms = await _chatService.GetRoomsAsync();
            return View(rooms);
        }


        public async Task<IActionResult> Messages(string id)
        {
            var limit = Convert.ToInt32(_configuration.GetSection("Configurations").GetSection("ChatMessageLimit").Value);
            var messages = await _chatService.GetMessagesAsync(id, 0, limit-1);
            var onlineUsers = await _userService.GetOnlineUsersAsync(id);
            
            var model = new ChatRoomViewModel
            {
                Messages = messages,
                Users  = onlineUsers
            };

            ViewBag.RoomId = id;
            return View(model);
        }

    }
}
