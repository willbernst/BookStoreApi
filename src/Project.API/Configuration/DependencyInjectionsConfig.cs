using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Project.API.Extensions;
using Project.Business.Interfaces;
using Project.Business.Notifications;
using Project.Business.Services;
using Project.Data.Context;
using Project.Data.Repository;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Project.API.Configuration
{
    public static class DependencyInjectionsConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MyDbContext>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();

            services.AddScoped<ICommunicator, Communicator>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IBookService, BookService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient < IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
