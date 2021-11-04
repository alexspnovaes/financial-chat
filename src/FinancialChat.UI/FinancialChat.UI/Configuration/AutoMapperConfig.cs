using Microsoft.Extensions.DependencyInjection;
using System;

namespace FinancialChat.UI.Configuration
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
