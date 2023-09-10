using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Product.Domain.Services;

namespace Sendeo.OnlineShop.Product.Domain.Installers
{
	public static class ServiceInstaller
	{
		public static void InstallServices(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
		}
	}
}
