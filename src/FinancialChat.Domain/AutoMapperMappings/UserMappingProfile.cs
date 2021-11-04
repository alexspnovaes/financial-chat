using AutoMapper;
using FinancialChat.Domain.Entities;
using FinancialChat.Domain.Models;

namespace FinancialChat.Domain.AutoMapperMappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
        }
    }
}
