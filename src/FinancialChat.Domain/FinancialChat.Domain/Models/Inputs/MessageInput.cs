﻿using System;

namespace FinancialChat.Domain.Models.Inputs
{
    public class MessageInput
    {
        public string From { get; set; }
        public string RoomId { get; set; }
        public DateTime Created { get; internal set; }
        public int Date { get; set; }
        public string Message { get; set; }
    }
}
