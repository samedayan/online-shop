using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sendeo.OnlineShop.Product.Domain.Settings.Client;
using Sendeo.OnlineShop.Product.Domain.Settings.MassTransit;
using Sendeo.OnlineShop.Product.Domain.Settings.Postgres;
using Sendeo.OnlineShop.Product.Domain.Settings.Validations;

namespace Sendeo.OnlineShop.Product.Domain.Installers
{
	public static class SettingsInstaller
	{
		public static void InstallSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.AddOptions<BusSettings>().ValidateOnStart();
			serviceCollection.AddOptions<PostgresSettings>().ValidateOnStart();
			serviceCollection.AddOptions<ClientSettings>().ValidateOnStart();

			serviceCollection.Configure<PostgresSettings>(configuration.GetSection(nameof(PostgresSettings)));
			var postgresSettings = configuration.Get<PostgresSettings>();
			configuration.GetSection(nameof(PostgresSettings)).Bind(postgresSettings);
			serviceCollection.AddSingleton(postgresSettings);

			serviceCollection.Configure<BusSettings>(configuration.GetSection(nameof(BusSettings)));
			var busSettings = configuration.Get<BusSettings>();
			configuration.GetSection(nameof(BusSettings)).Bind(busSettings);
			serviceCollection.AddSingleton(busSettings);

			serviceCollection.Configure<ClientSettings>(configuration.GetSection(nameof(ClientSettings)));
			var clientSettings = configuration.Get<ClientSettings>();
			configuration.GetSection(nameof(ClientSettings)).Bind(clientSettings);
			serviceCollection.AddSingleton(clientSettings);

			serviceCollection.AddSingleton<IValidateOptions<PostgresSettings>, PostgresSettingsValidation>();
			serviceCollection.AddSingleton<IValidateOptions<BusSettings>, MassTransitSettingsValidation>();
			serviceCollection.AddSingleton<IValidateOptions<ClientSettings>, ClientSettingsValidation>();

		}
	}
}
