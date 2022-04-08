using Microsoft.Extensions.DependencyInjection;
using Project.Business.Interfaces;
using Project.Business.Notifications;
using Project.Business.Services;
using Project.Data.Context;
using Project.Data.Repository;

namespace Project.API.Configuration
{
    public static class DependencyInjections
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

            return services;
        }
    }
}
