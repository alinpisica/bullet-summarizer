using BulletSummarizer.Infrastructure.Repository;
using BulletSummarizer.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Npgsql;
using DbUp;
using System.Reflection;
using BulletSummarizer.Infrastructure.Services.Interfaces;
using BulletSummarizer.Infrastructure.Services;
using BulletSummarizer.Infrastructure.Workers;

namespace BulletSummarizer.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDatabaseServices(configuration);

            services.AddSingleton<IReasoningService, ReasoningService>();
            services.AddSingleton<IPostmarkService, PostmarkService>();

            services.AddHostedService<SummarizationProcessor>();
            services.AddHostedService<QueuePollingProcessor>();
        }

        private static void RegisterDatabaseRepositories(this IServiceCollection services)
        {
            services.AddScoped<IInboundEmailRepository, InboundEmailRepository>();
            services.AddScoped<ISummarizationRepository, SummarizationRepository>();
        }

        private static void RegisterDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDatabaseRepositories();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IDbConnection>(s => new NpgsqlConnection(configuration.GetConnectionString("DbConnectionString")));

            services.AddScoped<IDbTransaction>(s =>
            {
                IDbConnection conn = s.GetRequiredService<IDbConnection>();
                conn.Open();

                return conn.BeginTransaction();
            });
        }

        public static void RegisterDbMigrations(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnectionString");

            EnsureDatabase.For.PostgresqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .PostgresqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
        }
    }
}
