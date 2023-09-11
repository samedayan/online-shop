using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Order.Domain.Installers;

namespace Sendeo.OnlineShop.Order.Application.Installers
{
	public static class ApplicationInstaller
	{
		public static void InstallApplication(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.InstallDomain(configuration);
			serviceCollection.InstallMediatr();
		}
	}
}
