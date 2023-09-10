using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Product.Consumer.Services;

namespace Sendeo.OnlineShop.Product.Consumer.Installers
{
	public static class ServiceInstaller
	{
		public static void InstallerServices(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.AddScoped<IProductService, ProductService>();
		}
	}
}
