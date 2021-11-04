using FinancialChat.Domain.Hubs;
using FinancialChat.Domain.Interfaces;
using FinancialChat.Domain.Interfaces.ExternalServices;
using FinancialChat.Domain.Interfaces.Services;
using FinancialChat.Domain.Services;
using FinancialChat.Infra.Data.Repositories;
using FinancialChat.Infra.ExternalServices;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialChat.UI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotifyStockService, NotifyStockService>();
            services.AddScoped<IStockApiExternalService, StockApiExternalService>();
            services.AddScoped<IChatHub, ChatHub>();
            #endregion

            #region repositories
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            #endregion
        }
    }
}
