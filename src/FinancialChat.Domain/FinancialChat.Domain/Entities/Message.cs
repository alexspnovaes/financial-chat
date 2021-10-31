using System;
using System.Collections.Generic;

namespace FinancialChat.Domain.Entities
{
    public class Message
    {
        public Guid Id {  get; set; }
        public int Destination { get; set; }
        public string Text { get; set; }
        public DateTime Created {  get; set; }
        public User Sender { get; set; }
    }
}
