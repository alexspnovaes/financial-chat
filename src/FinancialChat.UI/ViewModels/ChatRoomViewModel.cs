using FinancialChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialChat.UI.ViewModels
{
    public class ChatRoomViewModel
    {
        public List<MessageModel> Messages { get; set;  }   
        public List<string> Users {  get; set; }
    }
}
