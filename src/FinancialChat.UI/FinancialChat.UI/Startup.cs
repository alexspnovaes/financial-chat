using FinancialChat.Domain.Entities;
using FinancialChat.Domain.Hubs;
using FinancialChat.Domain.Models;
using FinancialChat.Infra.Context;
using FinancialChat.Infra.Data.Context;
using FinancialChat.UI.Configuration;
using FinancialChat.UI.Consumers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace FinancialChat.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMqConfig"));
            services.AddHostedService<ProcessStockMessageConsumer>();

            ConnectionMultiplexer redis = null;
            string redisConnectionUrl = null;
            {
                var redisEndpointUrl = (Environment.GetEnvironmentVariable("REDIS_ENDPOINT_URL") ?? "127.0.0.1:6379").Split(':');
                var redisHost = redisEndpointUrl[0];
                var redisPort = redisEndpointUrl[1];

                var redisPassword = Environment.GetEnvironmentVariable("REDIS_PASSWORD");
                if (redisPassword != null)
                {
                    redisConnectionUrl = $"{redisHost}:{redisPort},password={redisPassword}";
                }
                else
                {
                    redisConnectionUrl = $"{redisHost}:{redisPort}";
                }
                redis = ConnectionMultiplexer.Connect(redisConnectionUrl);

            }
            services.AddSingleton<IConnectionMultiplexer>(redis);

            services.AddDbContext<FinancialChatContext>(
                options => options.
                UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<FinancialChatContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
            });
            services
            .AddDataProtection()
            .PersistKeysToStackExchangeRedis(redis, "DataProtectionKeys");

            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = redisConnectionUrl;
                option.InstanceName = "RedisInstance";
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.Name = "FinanclChat";
            });

            services.AddRazorPages();
            services.AddSignalR(o => 
                o.EnableDetailedErrors = true
            );
            services.RegisterServices();
            services.RegisterMappings();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chatHub");
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            IConnectionMultiplexer redis = null;
            IHubContext<ChatHub> chatHub = null;
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                DbInitializer.Seed(serviceScope).Wait();
                chatHub = serviceScope.ServiceProvider.GetService<IHubContext<ChatHub>>();
                redis = serviceScope.ServiceProvider.GetService<IConnectionMultiplexer>();
            }

            var channel = redis.GetSubscriber().Subscribe("MESSAGES");
            channel.OnMessage(async message =>
            {
                try
                {
                    var mess = JsonConvert.DeserializeObject<PubSubMessageModel>(message.Message.ToString());
                    await chatHub.Clients.All.SendAsync(mess.Type, mess.Data);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e} ");
                }
            });
        }
    }
}
