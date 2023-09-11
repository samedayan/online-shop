using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Order.Domain.Services;

namespace Sendeo.OnlineShop.Order.Domain.Installers
{
	public static class ServiceInstaller
	{
		public static void InstallServices(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
		}
	}
}
