using FinancialChat.Domain.Interfaces;
using FinancialChat.Domain.Interfaces.Services;
using FinancialChat.Domain.Services;
using FinancialChat.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialChat.UI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region services
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region repositories
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            #endregion
        }
    }
}
