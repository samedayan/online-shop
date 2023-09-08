using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sendeo.OnlineShop.Customer.Domain.Repositories.User;
using Sendeo.OnlineShop.Customer.Domain.Settings.Postgres;
using Sendeo.OnlineShop.Customer.Persistence.PostgreSql.DataAccess;

namespace Sendeo.OnlineShop.Customer.Domain.Installers
{
	public static class RepositoryInstaller
	{
		public static void InstallRepositories(this IServiceCollection serviceCollection)
		{
			var postgresSettings = new PostgresSettings();
			var configuration = serviceCollection.BuildServiceProvider().GetRequiredService<IConfiguration>();
			configuration.GetSection(nameof(PostgresSettings)).Bind(postgresSettings);

			// https://docs.microsoft.com/en-us/ef/core/performance/advanced-performance-topics?tabs=with-di%2Cwith-constant#dbcontext-pooling
			serviceCollection.AddPooledDbContextFactory<CustomerDatabaseContext>(opt =>
			{
				opt.UseNpgsql(postgresSettings.ConnectionString, builder =>
				{
					builder.CommandTimeout(60000);
					builder.EnableRetryOnFailure(3);
				}).EnableSensitiveDataLogging();
				opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
				opt.LogTo(Console.WriteLine, LogLevel.Information);
			});

			serviceCollection.AddScoped<IUserRepository, UserRepository>();
		}
	}
}
