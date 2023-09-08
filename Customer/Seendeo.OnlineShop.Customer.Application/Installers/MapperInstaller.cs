using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Customer.Application.User.Mapping;

namespace Sendeo.OnlineShop.Customer.Application.Installers
{
	public static class MapperInstaller
	{
		public static void InstallMapper(this IServiceCollection serviceCollection)
		{
			UsergObjectMapping.Map();
		}
	}
}
