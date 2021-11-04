using FinancialChat.Domain.Interfaces.Services;
using FinancialChat.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

        public ChatController(IChatService chatService, IUserService userService)
        {
            _chatService = chatService;
            _userService = userService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var rooms = await _chatService.GetRoomsAsync();
            return View(rooms);
        }


        public async Task<IActionResult> Messages(string id)
        {
            var messages = await _chatService.GetMessagesAsync(id, 0, 49);
            var onlineUsers = await _userService.GetOnlineUsersButMeAsync(id);
            
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
