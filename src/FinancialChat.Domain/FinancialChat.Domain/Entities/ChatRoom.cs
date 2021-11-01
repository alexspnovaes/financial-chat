using System;
using System.Collections.Generic;

namespace FinancialChat.Domain.Entities
{
    public class ChatRoom
    {
        public Guid Id { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public string Name { get; set; }
    }
}
