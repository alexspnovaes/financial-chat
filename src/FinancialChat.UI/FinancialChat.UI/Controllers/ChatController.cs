using FinancialChat.Domain.Interfaces.Services;
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

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var rooms = await _chatService.GetRooms();
            return View(rooms);
        }


        public async Task<IActionResult> Messages(string id)
        {
            var messages = await _chatService.GetMessages(id, 0, 50);
            ViewBag.RoomId = id;
            return View(messages);
        }

    }
}
