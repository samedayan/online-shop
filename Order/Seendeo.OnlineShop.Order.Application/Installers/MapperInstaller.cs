using Microsoft.Extensions.DependencyInjection;
using Sendeo.OnlineShop.Order.Application.Order.Mapping;

namespace Sendeo.OnlineShop.Order.Application.Installers
{
    public static class MapperInstaller
	{
		public static void InstallMapper(this IServiceCollection serviceCollection)
		{
			OrderObjectMapping.Map();
		}
	}
}
