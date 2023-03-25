using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Concrete;
using Repositories.Contract;
using Services.Contract;
using Services;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace WebAPI.Extensitions
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection SqlServerConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<RepositoryContext>((options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer")
                    , b => b.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            });
        }

        public static void RepositoryServicesConfigure(this IServiceCollection services)
        {

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void BusinessServicesConfigure(this IServiceCollection services)
        {
            // Logger
            services.AddSingleton<ILoggerService,LoggerManager>();

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IBookService, BookManager>();
        }
    }
}
