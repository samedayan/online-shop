using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Customer.Domain.Installers;

namespace Sendeo.OnlineShop.Customer.Application.Installers
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
