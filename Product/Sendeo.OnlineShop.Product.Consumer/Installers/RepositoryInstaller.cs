using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sendeo.OnlineShop.Product.Domain.Repositories.Product;
using Sendeo.OnlineShop.Product.Domain.Repositories.User;
using Sendeo.OnlineShop.Product.Domain.Settings.Postgres;
using Sendeo.OnlineShop.Product.Persistence.PostgreSql.DataAccess;

namespace Sendeo.OnlineShop.Product.Consumer.Installers
{
	public static class RepositoryInstaller
	{
		public static void InstallRepositories(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			var postgresSettings = new PostgresSettings();
			var config = serviceCollection.BuildServiceProvider().GetRequiredService<IConfiguration>();
			config.GetSection(nameof(PostgresSettings)).Bind(postgresSettings);

			// https://docs.microsoft.com/en-us/ef/core/performance/advanced-performance-topics?tabs=with-di%2Cwith-constant#dbcontext-pooling
			serviceCollection.AddPooledDbContextFactory<ProductDatabaseContext>(opt =>
			{
				opt.UseNpgsql(postgresSettings.ConnectionString, builder =>
				{
					builder.CommandTimeout(60000);
					builder.EnableRetryOnFailure(3);
				}).EnableSensitiveDataLogging();
				opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
				opt.LogTo(Console.WriteLine, LogLevel.Information);
			});

			serviceCollection.AddScoped<IProductRepository, ProductRepository>();
		}
	}
}
