﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Sendeo.OnlineShop.Product.Consumer.HealtChecks;

namespace Sendeo.OnlineShop.Product.Consumer.Installers
{
	public static class HealthCheckInstaller
	{
		public static void InstallHealthCheck(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IHealthCheckPublisher, ConsumerHealthCheck>();
			serviceCollection.Configure<HealthCheckPublisherOptions>(options =>
			{
				options.Delay = TimeSpan.FromSeconds(5);
				options.Period = TimeSpan.FromSeconds(20);
			});
		}
	}
}
