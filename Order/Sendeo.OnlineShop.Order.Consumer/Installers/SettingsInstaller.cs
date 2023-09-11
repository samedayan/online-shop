using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sendeo.OnlineShop.Order.Domain.Settings.MassTransit;
using Sendeo.OnlineShop.Order.Domain.Settings.Postgres;
using Sendeo.OnlineShop.Order.Domain.Settings.Validations;

namespace Sendeo.OnlineShop.Order.Consumer.Installers
{
	public static class SettingsInstaller
	{
		public static void InstallSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.AddOptions<PostgresSettings>().ValidateOnStart();
			serviceCollection.AddOptions<BusSettings>().ValidateOnStart();

			serviceCollection.Configure<BusSettings>(configuration.GetSection(nameof(BusSettings)));
			var busSettings = configuration.Get<BusSettings>();
			configuration.GetSection(nameof(BusSettings)).Bind(busSettings);
			serviceCollection.AddSingleton(busSettings);

			serviceCollection.Configure<PostgresSettings>(configuration.GetSection("PostgresSettings"));
			var postgresSettings = configuration.Get<PostgresSettings>();
			configuration.GetSection(nameof(PostgresSettings)).Bind(postgresSettings);
			serviceCollection.AddSingleton(postgresSettings);

			serviceCollection.AddSingleton<IValidateOptions<PostgresSettings>, PostgresSettingsValidation>();
			serviceCollection.AddSingleton<IValidateOptions<BusSettings>, MassTransitSettingsValidation>();
		}
	}
}
