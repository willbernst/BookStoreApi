using Project.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Project.API.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLogginConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "0204c93521aa4f488462adb909791c0a";
                o.LogId = new Guid("bfb578f6-6957-416f-ad83-054ba439c9ab");
            });

            services.AddHealthChecks()
                .AddElmahIoPublisher(options =>
                {
                    options.ApiKey = "0204c93521aa4f488462adb909791c0a";
                    options.LogId = new Guid("bfb578f6-6957-416f-ad83-054ba439c9ab");
                    options.HeartbeatId = "API Suppliers";
                })
                .AddCheck("Books", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoS");

            services.AddHealthChecksUI()
                .AddSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));


            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}