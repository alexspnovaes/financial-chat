using AutoMapper;
using FinancialChat.Domain.Entities;
using FinancialChat.Domain.Models;

namespace FinancialChat.Domain.AutoMapperMappings
{
    public class ChatRoomMappingProfile : Profile
    {
        public ChatRoomMappingProfile()
        {
            CreateMap<ChatRoom, ChatRoomModel>().ReverseMap();
        }
    }
}
