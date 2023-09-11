using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Order.Domain.Installers;

namespace Sendeo.OnlineShop.Order.Domain.Installers
{
	public static class DomainInstaller
	{
		public static void InstallDomain(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.InstallSettings(configuration);
			serviceCollection.InstallMassTransit(configuration);
			serviceCollection.InstallRepositories();
			serviceCollection.InstallServices(configuration);
		}
	}
}
