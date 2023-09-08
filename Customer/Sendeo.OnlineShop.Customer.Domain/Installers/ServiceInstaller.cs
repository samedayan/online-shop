using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Customer.Domain.Services;

namespace Sendeo.OnlineShop.Customer.Domain.Installers
{
	public static class ServiceInstaller
	{
		public static void InstallServices(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
		}
	}
}
