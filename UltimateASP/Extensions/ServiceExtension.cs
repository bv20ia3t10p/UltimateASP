using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Service;
using Service.Contracts;
using System.Runtime.CompilerServices;

namespace UltimateASP.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builders =>
                builders.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
            });
        public static void ConfigureIISIntegration(this IServiceCollection services) => services.Configure<IISOptions>(options =>
        {

        });
        public static void ConfigureLoggingService  (this IServiceCollection services) => services.AddSingleton<ILoggerManager,LoggerManager>();
        public static void ConfigureRepositoryManager (this IServiceCollection services) => services.AddScoped<IRepositoryManager,RepositoryManager>();
        public static void ConfigureServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager, ServiceManager>();
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => services.AddDbContext<RepositoryContext>(opts
            => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder) =>
            builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));
    }
}
