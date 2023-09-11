﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sendeo.OnlineShop.Customer.Domain.Settings.Client;
using Sendeo.OnlineShop.Customer.Domain.Settings.MassTransit;
using Sendeo.OnlineShop.Customer.Domain.Settings.Postgres;
using Sendeo.OnlineShop.Customer.Domain.Settings.Validations;

namespace Sendeo.OnlineShop.Customer.Domain.Installers
{
	public static class SettingsInstaller
	{
		public static void InstallSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.AddOptions<PostgresSettings>().ValidateOnStart();
			serviceCollection.AddOptions<ClientSettings>().ValidateOnStart();

			serviceCollection.Configure<PostgresSettings>(configuration.GetSection(nameof(PostgresSettings)));
			var postgresSettings = configuration.Get<PostgresSettings>();
			configuration.GetSection(nameof(PostgresSettings)).Bind(postgresSettings);
			serviceCollection.AddSingleton(postgresSettings);

			serviceCollection.Configure<ClientSettings>(configuration.GetSection(nameof(ClientSettings)));
			var clientSettings = configuration.Get<ClientSettings>();
			configuration.GetSection(nameof(ClientSettings)).Bind(clientSettings);
			serviceCollection.AddSingleton(clientSettings);

			serviceCollection.AddSingleton<IValidateOptions<PostgresSettings>, PostgresSettingsValidation>();
			serviceCollection.AddSingleton<IValidateOptions<ClientSettings>, ClientSettingsValidation>();

		}
	}
}
