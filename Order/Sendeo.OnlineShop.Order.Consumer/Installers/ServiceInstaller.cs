using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Order.Consumer.Services;

namespace Sendeo.OnlineShop.Order.Consumer.Installers
{
	public static class ServiceInstaller
	{
		public static void InstallerServices(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.AddScoped<IOrderService, OrderService>();
		}
	}
}
