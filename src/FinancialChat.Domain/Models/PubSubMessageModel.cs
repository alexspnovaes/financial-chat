﻿namespace FinancialChat.Domain.Models
{
    public class PubSubMessageModel
    {
        public string Type { get; set; }
        public string Data { get; set; }
        public string ServerId { get; set; } = "123";
    }
}
